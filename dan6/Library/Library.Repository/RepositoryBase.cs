using Library.Common.Pagination;
using Library.Common.Sort;
using Library.Repository.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace Library.Repository
{
    public abstract class RepositoryBase<IModelT, FilterT>
    {
        protected SqlConnection _connection;

        protected abstract IQueryBuilder<IModelT> CreateQueryBuilder();

        protected void AddSearch(IQueryBuilder<IModelT> queryBuilder, string search, params string[] searchColumns)
        {
            if (search != null)
            {
                string expression = "";
                foreach (string column in searchColumns)
                {
                    expression += $"{column} LIKE @Search OR ";
                }
                expression = expression.Substring(0, expression.Length - 3);
                queryBuilder.AndWhere(expression, ("@Search", $"%{search}%"));
            }

        }

        protected void AddFilters(IQueryBuilder<IModelT> queryBuilder, FilterT filter)
        {
            foreach (PropertyInfo prop in filter.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                object value = prop.GetValue(filter);
                if (value != null)
                {
                    queryBuilder.AndWhere($"{prop.Name} LIKE @{prop.Name}", ($"@{prop.Name}", value));
                }
            }
        }

        protected void AddSort(IQueryBuilder<IModelT> queryBuilder, ISort sort, string defaultSort)
        {
            bool isValidColumn = false;
            foreach (PropertyInfo prop in typeof(IModelT).GetProperties())
            {
                if (prop.Name.ToLower() == sort?.SortBy?.ToLower())
                {
                    isValidColumn = true;
                }
            }
            sort.SortBy = isValidColumn ? sort.SortBy : defaultSort;
            if (sort?.Order?.ToUpper() != "ASC" && sort?.Order?.ToUpper() != "DESC")
            {
                sort.Order = "ASC";
            }
            queryBuilder.Sort(sort.SortBy, sort.Order.ToUpper());
        }

        protected void AddPagination(IQueryBuilder<IModelT> queryBuilder, IPagination pagination)
        {
            if (pagination?.PageNumber != null)
            {
                int offset = ((int)pagination.PageNumber - 1) * (int)pagination.PageSize;
                queryBuilder.Offset(offset).Limit((int)pagination.PageSize);
            }
        }
    }
}
