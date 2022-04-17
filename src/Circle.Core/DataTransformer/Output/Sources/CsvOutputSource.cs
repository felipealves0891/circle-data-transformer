using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Circle.Core.DataTransformer.Output.Sources
{
    public class CsvOutputSource : IOutputSource
    {
        private StreamWriter _write;
        private readonly char _delimiter;
        private readonly char _escape;
        private readonly char _textQualifier;

        public CsvOutputSource(
            StreamWriter stream,
            char delimiter = ';',
            char escape = '\\',
            char textQualifier = '\"'
        )
        {
            _write = stream;
            _delimiter = delimiter;
            _escape = escape;
            _textQualifier = textQualifier;
        }

        public CsvOutputSource(
            char delimiter = ';',
            char escape = '\\',
            char textQualifier = '\"'
        )
        {
            _delimiter = delimiter;
            _escape = escape;
            _textQualifier = textQualifier;
        }

        public void Open(string connectionString)
        {
            if (File.Exists(connectionString))
                File.Delete(connectionString);

            _write = new StreamWriter(connectionString, true, Encoding.GetEncoding("iso-8859-1"));
        }

        public void SetData(object[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                string column = data[i].ToString();
                string newColumn = "";
                for (int c = 0; c < column.Length; c++)
                {
                    if (column[c] == _textQualifier)
                        newColumn += _escape;

                    newColumn += column[c];
                }

                data[i] = newColumn;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(_textQualifier);
            builder.AppendJoin($"{_textQualifier}{_delimiter}{_textQualifier}", data);
            builder.Append(_textQualifier);

            _write.WriteLine(builder);
        }

        public void Close()
        {
            _write.Close();
        }

    }
}
