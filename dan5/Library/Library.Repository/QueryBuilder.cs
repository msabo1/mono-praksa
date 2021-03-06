using Library.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Library.Repository
{
    class QueryBuilder<ModelT> : IQueryBuilder<ModelT>
    {
        private bool _hasWhere = false;
        private string _sqlCommandString = "";
        private ICollection<(string Key, object Value)> _parameters = new List<(string, object)>();
        private string _tableName;
        private SqlConnection _connection;
        private Func<SqlDataReader, ModelT> _mappingFunction;

        public QueryBuilder(SqlConnection connection, Func<SqlDataReader, ModelT> mappingFunction)
        {
            _connection = connection;
            _mappingFunction = mappingFunction;
        }

        public IQueryBuilder<ModelT> Select(string table)
        {
            _tableName = table;
            _sqlCommandString += $"SELECT * FROM {table}";
            return this;
        }

        public IQueryBuilder<ModelT> LeftJoin(string table, string foreignKeyColumn, string relationColumn)
        {
            _sqlCommandString += $" LEFT JOIN {table} ON {_tableName}.{foreignKeyColumn} = {table}.{relationColumn}";
            return this;
        }

        public IQueryBuilder<ModelT> AndWhere(string statement, params (string Key, object Value)[] parameters)
        {
            if (!_hasWhere)
            {
                Where(statement);
            }
            else
            {
                _sqlCommandString += $" AND ({statement})";
            }

            AddParams(parameters);

            return this;
        }

        public IQueryBuilder<ModelT> OrWhere(string statement, params (string Key, object Value)[] parameters)
        {
            if (!_hasWhere)
            {
                Where(statement);
            }
            else
            {
                _sqlCommandString += $" OR ({statement})";
            }

            AddParams(parameters);
            return this;

        }

        public IQueryBuilder<ModelT> Where(string statement, params (string Key, object Value)[] parameters)
        {
            _sqlCommandString += $" WHERE ({statement})";
            _hasWhere = true;
            AddParams(parameters);
            return this;
        }

        public IQueryBuilder<ModelT> AddStatement(string statement, params (string Key, object Value)[] parameters)
        {
            _sqlCommandString += $" {statement}";
            AddParams(parameters);
            return this;
        }

        public string GetSqlCommandString()
        {
            return _sqlCommandString;
        }

        public SqlCommand GetSqlCommand()
        {
            SqlCommand cmd = new SqlCommand(_sqlCommandString, _connection);
            foreach (var param in _parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }

            return cmd;
        }

        public async Task<ICollection<ModelT>> GetManyAsync()
        {
            ICollection<ModelT> results = new List<ModelT>();
            SqlCommand cmd = GetSqlCommand();
            await _connection.OpenAsync();
            SqlDataReader sqlReader = await cmd.ExecuteReaderAsync();
            if (sqlReader.HasRows)
            {
                while (await sqlReader.ReadAsync())
                {
                    ModelT result = _mappingFunction(sqlReader);
                    results.Add(result);
                }
            }
            sqlReader.Close();
            _connection.Close();
            return results;
        }

        public async Task<ModelT> GetOneAsync()
        {
            ModelT result = default;
            SqlCommand cmd = GetSqlCommand();
            await _connection.OpenAsync();
            SqlDataReader sqlReader = await cmd.ExecuteReaderAsync();
            if (sqlReader.HasRows)
            {
                await sqlReader.ReadAsync();
                result = _mappingFunction(sqlReader);
            }
            sqlReader.Close();
            _connection.Close();
            return result;
        }

        public async Task<int> ExecuteNonQueryAsync()
        {
            SqlCommand cmd = GetSqlCommand();
            await _connection.OpenAsync();
            int result = await cmd.ExecuteNonQueryAsync();
            _connection.Close();
            return result;
        }

        private void AddParams(params (string Key, object Value)[] parameters)
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    _parameters.Add(param);
                }
            }
        }
    }
}
