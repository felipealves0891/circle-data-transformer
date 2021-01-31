using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Output.Sources
{
    public class TextOutputSource : IOutputSource
    {
        private StreamWriter _write;

        private char _delimiter;

        public TextOutputSource(char delimiter = ';')
        {
            _delimiter = delimiter;
        }

        public TextOutputSource(StreamWriter writer, char delimiter = ';')
        {
            _write = writer;
            _delimiter = delimiter;
        }

        public void Open(string connectionString)
        {
            if (File.Exists(connectionString))
                File.Delete(connectionString);

            _write = new StreamWriter(connectionString, true, Encoding.GetEncoding("iso-8859-1"));
        }

        public void SetData(object[] data)
        {
            StringBuilder builder = new StringBuilder();
            bool delimit = false;

            foreach(var column in data)
            {
                if (delimit)
                    builder.Append(_delimiter);

                builder.Append(column);
                delimit = true;
            }

            _write.WriteLine(builder);
                
        }

        public void Close()
        {
            _write.Close();
            _write.Dispose();
        }

    }
}
