using Library.Model.Common;
using Library.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Library.Repository
{
    class QueryBuilder<IModelT, ModelT> : IQueryBuilder<IModelT> where IModelT : IBaseModel where ModelT : IModelT
    {
        private bool _hasWhere = false;
        private string _sqlCommandString = "";
        private string _select = "";
        private string _from = "";
        private string _columns = "";
        private string _where = "";
        private string _join = "";
        private string _order = "";
        private string _limit = "";
        private string _offset = "";
        private ICollection<(string Key, object Value)> _parameters = new List<(string, object)>();
        private string _tableName;
        private SqlConnection _connection;
        private Func<SqlDataReader, IModelT> _mappingFunction;
        private Func<SqlDataReader, ModelT, ModelT> _mergeFunction;

        public QueryBuilder(SqlConnection connection, Func<SqlDataReader, IModelT> mappingFunction, Func<SqlDataReader, ModelT, ModelT> mergeFunction)
        {
            _connection = connection;
            _mappingFunction = mappingFunction;
            _mergeFunction = mergeFunction;
        }

        public IQueryBuilder<IModelT> Select()
        {
            _tableName = typeof(ModelT).Name;
            _select = "SELECT";
            _from = $" FROM {_tableName}";
            AddColumns<ModelT>(_tableName);
            _columns = _columns.Substring(1);
            return this;
        }

        public IQueryBuilder<IModelT> LeftJoinMany<T1, T2>()
        {
            string table1Name = typeof(T1).Name;
            string table2Name = typeof(T2).Name;
            AddColumns<T2>(table2Name);
            _join += $" LEFT JOIN {table2Name} ON {table1Name}.Id = {table2Name}.{table1Name}Id";
            return this;
        }

        public IQueryBuilder<IModelT> LeftJoinOne<T1, T2>()
        {
            string table1Name = typeof(T1).Name;
            string table2Name = typeof(T2).Name;
            AddColumns<T2>(table2Name);
            _join += $" LEFT JOIN {table2Name} ON {table2Name}.Id = {table1Name}.{table2Name}Id";
            return this;
        }

        public IQueryBuilder<IModelT> AndWhere(string statement, params (string Key, object Value)[] parameters)
        {
            if (!_hasWhere)
            {
                Where(statement);
            }
            else
            {
                _where += $" AND ({statement})";
            }

            AddParams(parameters);

            return this;
        }

        public IQueryBuilder<IModelT> OrWhere(string statement, params (string Key, object Value)[] parameters)
        {
            if (!_hasWhere)
            {
                Where(statement);
            }
            else
            {
                _where += $" OR ({statement})";
            }

            AddParams(parameters);
            return this;

        }

        public IQueryBuilder<IModelT> Where(string statement, params (string Key, object Value)[] parameters)
        {
            _where += $" WHERE ({statement})";
            _hasWhere = true;
            AddParams(parameters);
            return this;
        }

        public IQueryBuilder<IModelT> Sort(string sortBy, string order)
        {
            _order += $" ORDER BY {sortBy} {order}";
            return this;
        }

        public IQueryBuilder<IModelT> Limit(int limit)
        {
            _limit += $" FETCH NEXT {limit} ROWS ONLY";
            return this;
        }

        public IQueryBuilder<IModelT> Offset(int offset)
        {
            _offset += $" OFFSET {offset} ROWS";
            return this;
        }

        public IQueryBuilder<IModelT> AddStatement(string statement, params (string Key, object Value)[] parameters)
        {
            _sqlCommandString += $" {statement}";
            AddParams(parameters);
            return this;
        }

        public string GetSqlCommandString()
        {
            return $"{_select}{_columns}{_from}{_join}{_where}{_order}{_offset}{_limit}{_sqlCommandString}";
        }

        public SqlCommand GetSqlCommand()
        {
            SqlCommand cmd = new SqlCommand(GetSqlCommandString(), _connection);
            foreach (var param in _parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }

            return cmd;
        }

        public async Task<ICollection<IModelT>> GetManyAsync()
        {
            ICollection<IModelT> results = new List<IModelT>();
            SqlCommand cmd = GetSqlCommand();
            await _connection.OpenAsync();
            SqlDataReader sqlReader = await cmd.ExecuteReaderAsync();
            if (sqlReader.HasRows)
            {
                while (await sqlReader.ReadAsync())
                {
                    IModelT result = results.Where(r => r.Id == sqlReader.GetGuid(0)).FirstOrDefault();
                    if (result == null)
                    {
                        result = _mappingFunction(sqlReader);
                        results.Add(result);
                    }
                    else
                    {
                        result = _mergeFunction(sqlReader, (ModelT)result);
                    }
                }
            }
            sqlReader.Close();
            _connection.Close();
            return results;
        }

        public async Task<IModelT> GetOneAsync()
        {
            IModelT result = default;
            SqlCommand cmd = GetSqlCommand();
            await _connection.OpenAsync();
            SqlDataReader sqlReader = await cmd.ExecuteReaderAsync();
            if (sqlReader.HasRows)
            {
                while (await sqlReader.ReadAsync())
                {
                    if (result == null)
                    {
                        result = _mappingFunction(sqlReader);
                    }
                    else
                    {
                        result = _mergeFunction(sqlReader, (ModelT)result);
                    }
                }
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

        private void AddColumns<T>(string tableName)
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties().Where(p => p.PropertyType.IsClass || p.PropertyType == typeof(Guid) || p.PropertyType == typeof(string)))
            {
                _columns += $", {tableName}.{prop.Name} AS \"{tableName}.{prop.Name}\"";
            }
        }
    }
}
