using System;
using System.IO;
using Circle.Core.DataTransformer.Input.Sources;
using Circle.Core.DataTransformer.Output.Sources;
using Circle.Core.DataTransformer;
using Circle.Core.DataTransformer.Transformer.RowTransformations;
using Circle.Core.DataTransformer.Builder;
using Circle.Core.DataTransformer.Transformer.Transformations;

namespace Circle.MVP
{
    public class Program
    {
        static void Main(string[] args)
        {
            Run();
            RunWithBuilder();
        }

        private static void Run() 
        {
            string path = @"D:\db\";

            // Cria uma stream que sera lido
            var inputStream = new TextInputSource();
            inputStream.Open(Path.Combine(path, "input.csv"));

            // Cria uma stream que sera gravado
            var outputStream = new TextOutputSource();
            outputStream.Open(Path.Combine(path, "output.csv"));

            // Cria a transformação
            var transformation = new Transformation(inputStream, outputStream);
            transformation.AddRowTransformer(new RowTransformerSkipLine(0));
            transformation.AddTransformer(new TransformerNullToEmpty());
            transformation.AddTransformer(new TransformerToLower());

            // Adiciona um evento, com ele podemos acessar os estatos da transformação
            transformation.Diagnose += Transformation_Diagnose;

            // Executa a transformação e finaliza a transformação
            transformation.Execute();
            transformation.Dispose();

        }

        private static void RunWithBuilder() 
        {
            string path = @"D:\db\";

            // Cria uma stream que sera lido
            var inputStream = new TextInputSource();
            inputStream.Open(Path.Combine(path, "input.csv"));

            // Cria uma stream que sera gravado
            var outputStream = new TextOutputSource();
            outputStream.Open(Path.Combine(path, "output.csv"));

            // Cria um builder
            var builder = new TransformationBuilder();

            // Adiciona os streams e define as transformações que serão adicionadas
            var transformation = builder.SetInput(inputStream)
                                        .SetOutput(outputStream)
                                        .AddTransformerByType(typeof(TransformerNullToEmpty))
                                        .AddTransformerByType(typeof(TransformerToLower))
                                        .AddRowTransformerByType(typeof(RowTransformerSkipLine), 0)
                                        .Build();

            // Adiciona um evento, com ele podemos acessar os estatos da transformação
            transformation.Diagnose += Transformation_Diagnose;

            // Executa a transformação e finaliza a transformação
            transformation.Execute();
            transformation.Dispose();

        }

        private static void Transformation_Diagnose(object sender, EventArgs e)
        {
            var t = sender as Transformation;

            Console.Clear();
            Console.WriteLine($"\nRows : {t.Counter}");
            Console.WriteLine($"Time : {t.ElapsedTimeInSeconds()} s");
            Console.WriteLine($"Gen0 : {t.Collected(0)}");
            Console.WriteLine($"Gen1 : {t.Collected(1)}");
            Console.WriteLine($"Gen2 : {t.Collected(2)}");
            Console.WriteLine($"Memory : {t.MemoryUsed()} mb");

        }
    }

}
