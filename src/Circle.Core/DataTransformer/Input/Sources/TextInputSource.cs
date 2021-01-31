using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Input.Sources
{
    public class TextInputSource : IInputSource
    {
        private StreamReader _reader;

        private char _delimiter;

        private string _line;

        public TextInputSource(char delimiter = ';')
        {
            _delimiter = delimiter;
        }

        public TextInputSource(StreamReader reader, char delimiter = ';')
        {
            _reader = reader;
            _delimiter = delimiter;
        }

        public void Open(string connectionString)
        {
            if (!File.Exists(connectionString))
                throw new ArgumentException("Arquivo de entrada de dados não localizado!");

            _reader = new StreamReader(connectionString, Encoding.GetEncoding("iso-8859-1"));
        }

        public bool Next()
        {
            if (_reader == null)
                throw new Exception("Não a fluxo de dados para leitura.");

            return (_line = _reader.ReadLine()) != null;
        }

        public object[] GetData()
        {
            if (_line == null)
                return null;

            return _line.Split(_delimiter);
        }

        public void Close()
        {
            _reader.Close();
            _reader.Dispose();
            _reader = null;
        }

    }
}
