using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Circle.MVP
{
    public class CsvReader : IDisposable
    {
        private readonly StreamReader _stream;

        private readonly char[] _endline;

        private readonly char _delimiter;

        private readonly char _textQualifier;

        private readonly char _escape;

        private char[] _chunk;

        private int _counter;

        private bool _isText;

        public CsvReader(StreamReader stream, char[] endline, char delimiter, char textQualifier, char escape)
        {
            _stream = stream;
            _endline = endline;
            _delimiter = delimiter;
            _textQualifier = textQualifier;
            _escape = escape;
            _counter = -1;
            _chunk = new char[0];
        }

        public object[] Read()
        {
            List<object> columns = new List<object>();
            
            while(_stream.Peek() >= 0)
            {
                char nextChunk = (char)_stream.Read();

                if (IsStartOrEndText(nextChunk)) 
                {
                    _isText = !_isText;
                    continue;
                }
                
                if (IsEndColumn(nextChunk)) 
                {
                    columns.Add(GetColumn());
                    continue;
                }

                if (IsEndLine(nextChunk)) 
                {
                    _chunk = _chunk.Take(_counter).ToArray();
                    columns.Add(GetColumn());
                    break;
                }
                
                Array.Resize<char>(ref _chunk, _chunk.Length + 1);
                _counter++;
                _chunk[_counter] = nextChunk;
                
            }

            if (columns.Count == 0)
                return null;

            return columns.ToArray();
        }

        private bool IsStartOrEndText(char c)
        {
            if (_counter < 0)
                return _textQualifier.Equals(c);

            return _textQualifier.Equals(c) && !_escape.Equals(_chunk[_counter]);
        }

        private bool IsEndColumn(char c)
        {
            if (_counter < 0)
                return _delimiter.Equals(c);

            return _delimiter.Equals(c) && !_escape.Equals(_chunk[_counter]) && !_isText;
        }

        private bool IsEndLine(char c)
        {
            if (_counter < 0)
                return false;

            return _endline[0].Equals(_chunk[_counter]) 
                && _endline[1].Equals(c) 
                && !_isText;
        }

        private string GetColumn()
        {
            StringBuilder builder = new StringBuilder(_chunk.Length);
            builder.Append(_chunk);
            _chunk = new char[0];
            _counter = -1;
            return builder.ToString();
        }

        public void Dispose()
        {
            if(_stream != null)
                _stream.Close();
        }
    }
}
