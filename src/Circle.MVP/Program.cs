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
            string path = @"D:\db\";
            string query = "insert into Teste values (@1, @2, @3, @4, @5, @6)";

            var inputStream = new TextInputSource();
            inputStream.Open(Path.Combine(path, "input5.csv"));

            var outputStream = new SqlServerOutputSource(query, new string[] { "@1", "@2", "@3", "@4", "@5", "@6" });
            outputStream.Open("Server=(LocalDB)\\MSSQLLocalDB;Database=dbDataTransformer;Trusted_Connection=True;");

            var builder = new TransformationBuilder();
            var transformation = builder.SetInput(inputStream)
                                       .SetOutput(outputStream)
                                       .AddTransformerByType(typeof(TransformerNullToEmpty))
                                       .AddTransformerByType(typeof(TransformerToLower))
                                       .AddRowTransformerByType(typeof(RowTransformerSkipLine), 0)
                                       .Build();

            transformation.Diagnose += Transformation_Diagnose;
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
