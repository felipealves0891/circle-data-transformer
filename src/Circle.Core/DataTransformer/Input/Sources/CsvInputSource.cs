using System;
using System.Data.Odbc;
using System.Data;

namespace Circle.Core.DataTransformer.Input.Sources
{
    /// <summary>
    /// ;Type connection
    /// ;Connection odbc 
    /// ;Driver = Microsoft Access Text Driver (*.txt, *.csv)
    /// </summary>
    public class CsvInputSource : IInputSource
    {
        private OdbcConnection _connection;

        private OdbcCommand _command;

        private OdbcDataReader _reader;

        private bool _header = true;

        ///<summary>
        /// Query =
        /// select column1, column2 from filename.ext
        ///</summary>
        public CsvInputSource(string query)
        {
            _command = new OdbcCommand();
            _command.CommandType = CommandType.Text;
            _command.CommandText = query;
        }

        /// <summary>
        /// ConnectionString=
        /// file directory
        /// </summary>
        /// <param name="connectionString"></param>
        public void Open(string connectionString)
        {
            connectionString = $"Driver=Microsoft Access Text Driver (*.txt, *.csv);Dbq={connectionString}; Extensions=asc,csv,tab,txt;FMT=CSVDelimited;";
            _connection = new OdbcConnection(connectionString);
            _connection.Open();
            _command.Connection = _connection;
            _reader = _command.ExecuteReader();
        }

        public bool Next()
        {
            if (_header)
                return true;

            return _reader.Read();
        }

        public object[] GetData()
        {
            int len = _reader.FieldCount;
            object[] data = new object[len];

            for (int i = 0; i < len; i++)
            {
                if (_header)
                    data[i] = _reader.GetName(i);
                else
                    data[i] = _reader.GetValue(i);
            }

            _header = false;
            return data;
        }

        public void Close()
        {
            _command.Dispose();
            _reader.Close();
            _connection.Close();
        }
    }
}
