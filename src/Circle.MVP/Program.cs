using System;
using System.IO;
using Circle.Core.DataTransformer.Input.Sources;
using Circle.Core.DataTransformer.Output.Sources;
using Circle.Core.DataTransformer.Transformer.Transformations;
using Circle.Core.DataTransformer.Transformer.RowTransformations;
using Circle.Core.DataTransformer;
using System.Diagnostics;
using Circle.Core.DataTransformer.Input;
using Circle.Core.DataTransformer.Output;

namespace Circle.MVP
{
    class Program
    {
        static void Main(string[] args)
        {
            //Inicia o processo
            var before0 = GC.CollectionCount(0);
            var before1 = GC.CollectionCount(1);
            var before2 = GC.CollectionCount(2);

            var sw = new Stopwatch();

            sw.Start();

            Test();

            sw.Stop();

            Console.WriteLine($"\n Time : {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Gen0 : {GC.CollectionCount(0) - before0}");
            Console.WriteLine($"Gen1 : {GC.CollectionCount(1) - before1}");
            Console.WriteLine($"Gen2 : {GC.CollectionCount(2) - before2}");
            Console.WriteLine($"Memory : {Process.GetCurrentProcess().WorkingSet64 /1024 / 1024} mb");

        }

        static void Test()
        {
            string path = @"D:\db\";
            string query = "select * from [input.csv]";

            var inputStream = new CsvInputSource(query);
            inputStream.Open(path);

            var outputStream = new TextOutputSource();
            outputStream.Open(Path.Combine(path, "output.csv"));

            var transformation = new Transformation(inputStream, outputStream);
            transformation.Execute();

        }

    }

}
