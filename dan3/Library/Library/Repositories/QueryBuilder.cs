using System.Collections.Generic;
using System.Data.SqlClient;

namespace Day2.Repositories
{
    public class QueryBuilder
    {
        private bool _hasWhere = false;
        private string _sqlCommandString = "";
        private List<(string Key, object Value)> _parameters = new List<(string, object)>();
        private string _tableName;

        public QueryBuilder Select(string table)
        {
            _tableName = table;
            _sqlCommandString += $"SELECT * FROM {table}";
            return this;
        }

        public QueryBuilder LeftJoin(string table, string foreignKeyColumn, string relationColumn)
        {
            _sqlCommandString += $" LEFT JOIN {table} ON {_tableName}.{foreignKeyColumn} = {table}.{relationColumn}";
            return this;
        }

        public QueryBuilder AndWhere(string statement, params (string Key, object Value)[] parameters)
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

        public QueryBuilder OrWhere(string statement, params (string Key, object Value)[] parameters)
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

        public QueryBuilder Where(string statement, params (string Key, object Value)[] parameters)
        {
            _sqlCommandString += $" WHERE ({statement})";
            _hasWhere = true;
            AddParams(parameters);
            return this;
        }

        public QueryBuilder AddStatement(string statement, params (string Key, object Value)[] parameters)
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
            SqlCommand cmd = new SqlCommand(_sqlCommandString);
            foreach (var param in _parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }

            return cmd;
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
