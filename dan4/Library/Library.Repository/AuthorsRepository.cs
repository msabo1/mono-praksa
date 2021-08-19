using Library.Model.Author;
using Library.Model.Common;
using Library.Model.Common.Author;
using Library.Repository.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Library.Repository
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
        public IAuthor Create(ICreateAuthorDto createAuthorDto)
        {
            Guid id = Guid.NewGuid();
            IAuthor author = new Author();
            author.Id = id;
            author.Name = createAuthorDto.Name;
            author.Gender = createAuthorDto.Gender;
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement(
                "INSERT INTO Author VALUES(@Id, @Name, @Gender)",
                ("@Id", author.Id),
                ("@Name", author.Name),
                ("@Gender", author.Gender)
            );
            queryBuilder.ExecuteNonQuery();
            return author;
        }

        public ICollection<IAuthor> Get(IQueryAuthorsDto queryAuthorsDto = null)
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
            return queryBuilder.GetMany(); ;
        }

        public IAuthor GetById(Guid id)
        {
            IQueryBuilder<IAuthor> querBuilder = CreateQueryBuilder();
            querBuilder.Select("Author").Where("Id = @Id", ("@Id", id));
            return querBuilder.GetOne();
        }

        public IAuthor Update(Guid id, IUpdateAuthorDto updateAuthorDto)
        {
            IAuthor author = GetById(id);
            if (author == null)
            {
                return null;
            }

            if (updateAuthorDto.Name != null)
            {
                author.Name = updateAuthorDto.Name;
            }
            if (updateAuthorDto.Gender != null)
            {
                author.Gender = updateAuthorDto.Gender;
            }
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement(
            "UPDATE Author SET Name = @Name, Gender = @Gender WHERE Id = @Id",
            ("@Id", id),
            ("@Name", author.Name),
            ("@Gender", author.Gender)
        );
            queryBuilder.ExecuteNonQuery();
            return author;
        }

        public void Delete(Guid id)
        {
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement("DELETE FROM Author WHERE Id = @Id", ("@Id", id));
            int rowsAffected = queryBuilder.ExecuteNonQuery();
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
