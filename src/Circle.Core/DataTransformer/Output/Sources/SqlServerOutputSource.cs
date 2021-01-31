using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Circle.Core.DataTransformer.Output.Sources
{
    public class SqlServerOutputSource : IOutputSource
    {
        private SqlConnection _connection;

        private SqlCommand _command;

        private SqlTransaction _transaction;

        private string[] _parameterIdentifier;

        private int _transactions;

        private int _lote;

        public SqlServerOutputSource(string query, string[] parameterIdentifier, int lote = 1000)
        {
            _parameterIdentifier = parameterIdentifier;
            _transactions = 0;
            _lote = lote;

            _command = new SqlCommand();
            _command.CommandType = CommandType.Text;
            _command.CommandText = query;

        }

        public void Open(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _command.Connection = _connection;
            InitTransaction();
        }

        public void SetData(object[] data)
        {
            var command = _command.Clone();
            for (int i = 0; i < data.Length; i++)
                command.Parameters.AddWithValue(_parameterIdentifier[i], data[i]);

            command.ExecuteNonQuery();
            command.Dispose();
            _transactions++;

            if(_transactions == _lote)
            {
                _transaction.Commit();
                InitTransaction();
            }
        }

        public void Close()
        {
            _transaction.Commit();
            _connection.Close();
            _command.Dispose();
        }

        private void InitTransaction()
        {
            _transaction = _connection.BeginTransaction();
            _command.Transaction = _transaction;
            _transactions = 0;
        }

    }
}
