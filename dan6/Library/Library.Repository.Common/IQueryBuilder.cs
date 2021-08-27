
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Library.Repository.Common
{
    public interface IQueryBuilder<ModelT>
    {
        IQueryBuilder<ModelT> Select();
        IQueryBuilder<ModelT> LeftJoinMany<T1, T2>();
        IQueryBuilder<ModelT> LeftJoinOne<T1, T2>();
        IQueryBuilder<ModelT> AndWhere(string statement, params (string Key, object Value)[] parameters);
        IQueryBuilder<ModelT> OrWhere(string statement, params (string Key, object Value)[] parameters);
        IQueryBuilder<ModelT> Where(string statement, params (string Key, object Value)[] parameters);
        IQueryBuilder<ModelT> Sort(string sortBy, string order);
        IQueryBuilder<ModelT> Limit(int limit);
        IQueryBuilder<ModelT> Offset(int offset);
        IQueryBuilder<ModelT> AddStatement(string statement, params (string Key, object Value)[] parameters);
        string GetSqlCommandString();
        SqlCommand GetSqlCommand();
        Task<ICollection<ModelT>> GetManyAsync();
        Task<ModelT> GetOneAsync();
        Task<int> ExecuteNonQueryAsync();

    }
}
