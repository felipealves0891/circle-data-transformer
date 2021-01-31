using System.Data;
using System.Data.SqlClient;

namespace Circle.Core.DataTransformer.Input.Sources
{
    public class SqlServerInputSource : IInputSource
    {
        private SqlConnection _connection;

        private SqlCommand _command;

        private SqlDataReader _reader;

        private bool _header;

        public SqlServerInputSource(string query)
        {
            _command = new SqlCommand();
            _command.CommandType = CommandType.Text;
            _command.CommandText = query;
        }

        public void Open(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _command.Connection = _connection;
            _reader = _command.ExecuteReader();
            _header = true;
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
            _connection.Close();
        }

    }
}
