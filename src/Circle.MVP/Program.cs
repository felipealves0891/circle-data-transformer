using System;
using System.IO;
using Circle.Core.DataTransformer.Input.Sources;
using Circle.Core.DataTransformer.Output.Sources;
using Circle.Core.DataTransformer;
using Circle.Core.DataTransformer.Transformer.RowTransformations;
using Circle.Core.DataTransformer.Builder;
using Circle.Core.DataTransformer.Transformer.Transformations;
using System.Collections.Generic;
using Circle.Core.DataTransformer.Input;

namespace Circle.MVP
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunCsvInput();
        }

        private static void Run() 
        {
            // Cria uma stream que sera lido
            var inputStream = new TextInputSource();
            inputStream.Open(@"D:\Source\DataSets\ml-25m\ratings.csv");

            // Cria uma stream que sera gravado
            var outputStream = new TextOutputSource();
            outputStream.Open(@"D:\Source\DataSets\Outputs\ratings.csv");

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

        private static void RunJoinSources()
        {
            string path = @"D:\db\join\";

            List<IInputSource> sources = new List<IInputSource>();

            // Cria uma stream que sera lido
            var inputStream1 = new TextInputSource();
            inputStream1.Open(Path.Combine(path, "input1.csv"));
            sources.Add(inputStream1);

            var inputStream2 = new TextInputSource();
            inputStream2.Open(Path.Combine(path, "input2.csv"));
            sources.Add(inputStream2);

            var inputStream3 = new TextInputSource();
            inputStream3.Open(Path.Combine(path, "input3.csv"));
            sources.Add(inputStream3);

            var inputStream4 = new TextInputSource();
            inputStream4.Open(Path.Combine(path, "input4.csv"));
            sources.Add(inputStream4);

            var inputStream5 = new TextInputSource();
            inputStream5.Open(Path.Combine(path, "input5.csv"));
            sources.Add(inputStream5);

            var inputStream6 = new TextInputSource();
            inputStream6.Open(Path.Combine(path, "input6.csv"));
            sources.Add(inputStream6);

            var join = new JoinInputSource(sources);

            // Cria uma stream que sera gravado
            var outputStream = new TextOutputSource();
            outputStream.Open(Path.Combine(path, "output.csv"));

            // Cria a transformação
            var transformation = new Transformation(join, outputStream);
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

            // Adiciona os streams e define as transformações que serão adicionadas
            var transformation = TransformationBuilder.Instance()
                                        .SetInput(inputStream)
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


        private static void RunCsvInput()
        {
            // Cria uma stream que sera lido
            var inputStream = new CsvInputSource("select * from [movies.csv]");
            inputStream.Open(@"D:\Source\DataSets\ml-25m\");

            // Cria uma stream que sera gravado
            var outputStream = new TextOutputSource();
            outputStream.Open(@"D:\Source\DataSets\Outputs\output.csv");

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


        private static void Transformation_Diagnose(object sender, EventArgs e)
        {
            var t = sender as Transformation;

            var time = (t.ElapsedTimeInSeconds() / 60).ToString("0#") + ":" + (t.ElapsedTimeInSeconds() % 60).ToString("0#");

            Console.Clear();
            Console.WriteLine($"\nRows : {t.Counter.ToString("n0")}");
            Console.WriteLine($@"R\S  : {(t.Counter / t.ElapsedTimeInSeconds()).ToString("n0")}");
            Console.WriteLine($"Time : {time}");
            Console.WriteLine($"Gen0 : {t.Collected(0)}");
            Console.WriteLine($"Gen1 : {t.Collected(1)}");
            Console.WriteLine($"Gen2 : {t.Collected(2)}");
            Console.WriteLine($"Memory : {t.MemoryUsed()} mb");

        }
    }

}
