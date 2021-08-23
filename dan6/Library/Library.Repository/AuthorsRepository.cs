using Library.Model.Author;
using Library.Model.Common;
using Library.Model.Common.Author;
using Library.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private SqlConnection _connection;

        public AuthorsRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IAuthor> CreateAsync(IAuthor author)
        {
            author.Id = Guid.NewGuid();
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement(
                "INSERT INTO Author VALUES(@Id, @Name, @Gender)",
                ("@Id", author.Id),
                ("@Name", author.Name),
                ("@Gender", author.Gender)
            );
            await queryBuilder.ExecuteNonQueryAsync();
            return author;
        }

        public async Task<ICollection<IAuthor>> GetAsync(IQueryAuthorsDto queryAuthorsDto = null)
        {
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.Select("Author");
            if (queryAuthorsDto?.Search != null)
            {
                queryBuilder.OrWhere("Name LIKE @Search OR Gender LIKE @Search", ("@Search", $"%{queryAuthorsDto.Search}%"));
            }
            if (queryAuthorsDto?.Gender != null)
            {
                queryBuilder.AndWhere("Gender LIKE @Gender", ("@Gender", queryAuthorsDto.Gender));
            }
            if (queryAuthorsDto?.SortBy != null)
            {
                if (queryAuthorsDto.Order?.ToUpper() != "ASC" || queryAuthorsDto.Order?.ToUpper() != "DESC")
                {
                    queryAuthorsDto.Order = "ASC";
                }
                // can't do param bindning here, should be validated at request level...
                queryBuilder.AddStatement($"ORDER BY {queryAuthorsDto.SortBy} {queryAuthorsDto.Order.ToUpper()}");
            }
            return await queryBuilder.GetManyAsync(); ;
        }

        public async Task<IAuthor> GetByIdAsync(Guid id)
        {
            IQueryBuilder<IAuthor> querBuilder = CreateQueryBuilder();
            querBuilder.Select("Author").Where("Id = @Id", ("@Id", id));
            return await querBuilder.GetOneAsync();
        }

        public async Task<IAuthor> UpdateAsync(IAuthor author)
        {
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement(
                "UPDATE Author SET Name = @Name, Gender = @Gender WHERE Id = @Id",
                ("@Id", author.Id),
                ("@Name", author.Name),
                ("@Gender", author.Gender)
            );
            await queryBuilder.ExecuteNonQueryAsync();
            return author;
        }

        public async Task DeleteAsync(Guid id)
        {
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement("DELETE FROM Author WHERE Id = @Id", ("@Id", id));
            int rowsAffected = await queryBuilder.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new Exception();
            }
        }

        private IAuthor MapDataReaderRowToAuthor(SqlDataReader reader)
        {
            IAuthor author = new Author();
            author.Id = reader.GetGuid(0);
            author.Name = reader.GetString(1);
            author.Gender = reader.GetString(2);
            return author;
        }

        private IQueryBuilder<IAuthor> CreateQueryBuilder()
        {
            return new QueryBuilder<IAuthor>(_connection, MapDataReaderRowToAuthor);
        }
    }
}
