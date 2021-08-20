using Library.Model.Author;
using Library.Model.Book;
using Library.Model.Common;
using Library.Model.Common.Book;
using Library.Repository.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
        public async Task<IBook> CreateAsync(IBook book)
        {
            book.Id = Guid.NewGuid(); ;
            IQueryBuilder<IBook> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement(
                "INSERT INTO Book VALUES(@Id, @Name, @AuthorId)",
                ("@Id", book.Id),
                ("@Name", book.Name),
                ("@AuthorId", book.AuthorId)
            );
            await queryBuilder.ExecuteNonQueryAsync();
            return book;
        }

        public async Task<ICollection<IBook>> GetAsync(IQueryBooksDto queryBooksDto = null)
        {
            IQueryBuilder<IBook> queryBuilder = new QueryBuilder<IBook>(_connection, MapDataReaderRowToBook);
            queryBuilder.Select("Book").LeftJoin("Author", "AuthorId", "Id");
            if (queryBooksDto?.Search != null)
            {
                queryBuilder.OrWhere("Name LIKE @Search", ("@Search", $"%{queryBooksDto.Search}%"));
            }
            if (queryBooksDto?.SortBy != null)
            {
                if (queryBooksDto.Order?.ToUpper() != "ASC" || queryBooksDto.Order?.ToUpper() != "DESC")
                {
                    queryBooksDto.Order = "ASC";
                }
                // can't do param bindning here, should be validated at request level...
                queryBuilder.AddStatement($"ORDER BY {queryBooksDto.SortBy} {queryBooksDto.Order.ToUpper()}");
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
                "UPDATE Book SET Name = @Name, AuthorId = @AuthorId WHERE Id = @Id",
                ("@Id", book.Id),
                ("@Name", book.Name),
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

        private IBook MapDataReaderRowToBook(SqlDataReader reader)
        {
            IBook book = new Book();
            book.Id = reader.GetGuid(0);
            book.Name = reader.GetString(1);
            book.AuthorId = reader.GetGuid(2);
            book.Author = new Author();
            book.Author.Id = reader.GetGuid(3);
            book.Author.Name = reader.GetString(4);
            book.Author.Gender = reader.GetString(5);
            return book;
        }

        private IQueryBuilder<IBook> CreateQueryBuilder()
        {
            return new QueryBuilder<IBook>(_connection, MapDataReaderRowToBook);
        }
    }
}
