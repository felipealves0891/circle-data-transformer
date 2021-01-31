using System;
using System.IO;
using Circle.Core.DataTransformer.Input.Sources;
using Circle.Core.DataTransformer.Output.Sources;
using Circle.Core.DataTransformer;

namespace Circle.MVP
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\db\";

            var inputStream = new TextInputSource();
            inputStream.Open(Path.Combine(path, "input2.csv"));

            var outputStream = new TextOutputSource();
            outputStream.Open(Path.Combine(path, "output.csv"));

            var transformation = new Transformation(inputStream, outputStream);
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
