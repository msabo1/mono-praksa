
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Library.Repository.Common
{
    public interface IQueryBuilder<ModelT>
    {
        IQueryBuilder<ModelT> Select(string table);
        IQueryBuilder<ModelT> LeftJoin(string table, string foreignKeyColumn, string relationColumn);
        IQueryBuilder<ModelT> AndWhere(string statement, params (string Key, object Value)[] parameters);
        IQueryBuilder<ModelT> OrWhere(string statement, params (string Key, object Value)[] parameters);
        IQueryBuilder<ModelT> Where(string statement, params (string Key, object Value)[] parameters);
        IQueryBuilder<ModelT> AddStatement(string statement, params (string Key, object Value)[] parameters);
        string GetSqlCommandString();
        SqlCommand GetSqlCommand();
        ICollection<ModelT> GetMany();
        ModelT GetOne();
        int ExecuteNonQuery();

    }
}
