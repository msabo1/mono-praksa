using AutoMapper;
using Library.Common.Filters;
using Library.Common.Pagination;
using Library.Common.Sort;
using Library.Model;
using Library.Model.Common;
using Library.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private SqlConnection _connection;
        private IMapper _mapper;
        public BooksRepository(SqlConnection connection, IMapper mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }
        public async Task<IBook> CreateAsync(IBook book)
        {
            book.Id = Guid.NewGuid(); ;
            IQueryBuilder<IBook> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement(
                "INSERT INTO Book VALUES(@Id, @Name, @AuthorId)",
                ("@Id", book.Id),
                ("@Name", book.Title),
                ("@AuthorId", book.AuthorId)
            );
            await queryBuilder.ExecuteNonQueryAsync();
            return book;
        }

        public async Task<ICollection<IBook>> GetAsync(ISort sort = null, IPagination pagination = null, IBookFilter filter = null)
        {
            IQueryBuilder<IBook> queryBuilder = CreateQueryBuilder();
            queryBuilder.Select("Book").LeftJoin("Author", "AuthorId", "Id");
            if (filter?.Search != null)
            {
                queryBuilder.OrWhere("Title LIKE @Search", ("@Search", $"%{filter.Search}%"));
            }
            queryBuilder.Sort(sort.SortBy ?? "Title", sort.Order.ToUpper());
            if (pagination?.PageNumber != null)
            {
                int offset = ((int)pagination.PageNumber - 1) * (int)pagination.PageSize;
                queryBuilder.Offset(offset).Limit((int)pagination.PageSize);
            }
            return await queryBuilder.GetManyAsync();
        }

        public async Task<IBook> GetByIdAsync(Guid id)
        {
            IQueryBuilder<IBook> querBuilder = CreateQueryBuilder();
            querBuilder.Select("Book").LeftJoin("Author", "AuthorId", "Id").Where("Book.Id = @Id", ("@Id", id));
            return await querBuilder.GetOneAsync();
        }

        public async Task<IBook> UpdateAsync(IBook book)
        {
            IQueryBuilder<IBook> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement(
                "UPDATE Book SET Title = @Title, AuthorId = @AuthorId WHERE Id = @Id",
                ("@Id", book.Id),
                ("@Title", book.Title),
                ("@AuthorId", book.AuthorId)
            );
            await queryBuilder.ExecuteNonQueryAsync();
            return book;
        }

        public async Task DeleteAsync(Guid id)
        {
            IQueryBuilder<IBook> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement("DELETE FROM Book WHERE Id = @Id", ("@Id", id));
            int rowsAffected = await queryBuilder.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new Exception();
            }
        }

        private IQueryBuilder<IBook> CreateQueryBuilder()
        {
            return new QueryBuilder<IBook>(_connection, _mapper.Map<IDataRecord, Book>);
        }
    }
}
