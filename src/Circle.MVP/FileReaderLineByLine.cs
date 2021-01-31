using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Circle.MVP
{
    public class FileReaderLineByLine : IDisposable
    {
        private StreamReader _reader;

        public int Counter { get; private set; }

        public string Line { get; private set; }

        public string[] Headers { get; private set; }

        public object[] Columns { get; private set; }

        // <TextLine, NumberLine>
        public Func<string, int, string> LineRead { get; set; }

        // <TextColumn, TextHeader, NumberColumn, NumberLine>
        public Func<string, string, int, int, object> ColumnRead { get; set; }

        public FileReaderLineByLine(string filename, bool containsHeader)
        {
            _reader = new StreamReader(filename);
            Counter = -1;

            if (containsHeader)
            {
                if (!Next())
                    return;

                Headers = Columns.Select(x => x.ToString()).ToArray();
                Columns = null;
                Line = null;
            }

        }

        public bool Next(char delimiter = ';')
        {
            string currentline;
            Counter++;

            if ((currentline = _reader.ReadLine()) == null)
                return false;

            Line = currentline;
            if (LineRead != null)
                Line = LineRead(Line, Counter);

            Columns = currentline.Split(delimiter);
            if (ColumnRead != null)
            {
                Columns = Columns.Select((col, i) =>
                                ColumnRead(col.ToString(), Headers[i], i, Counter)).ToArray();
            }

            return true;
        }

        public void Dispose()
        {
            _reader.Close();
        }
    }
}
