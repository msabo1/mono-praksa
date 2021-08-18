using Day2.Models.Author;
using Day2.Repositories;
using Library.Models.Author;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Library.DataStorage
{
    public class AuthorsRepository
    {
        private static SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
        public static Author Create(CreateAuthorDto createAuthorDto)
        {
            Guid id = Guid.NewGuid();
            Author author = new Author();
            author.Id = id;
            author.Name = createAuthorDto.Name;
            author.Gender = createAuthorDto.Gender;
            SqlCommand sqlCmd = CreateSqlCommand(
                "INSERT INTO Author VALUES(@Id, @Name, @Gender)",
                ("@Id", author.Id),
                ("@Name", author.Name),
                ("@Gender", author.Gender)
            );
            _connection.Open();
            sqlCmd.ExecuteNonQuery();
            _connection.Close();
            return author;
        }

        public static ICollection<Author> Get(QueryAuthorsDto queryAuthorsDto = null)
        {
            ICollection<Author> authors = new List<Author>();
            QueryBuilder queryBuilder = new QueryBuilder();
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

            SqlCommand sqlCmd = queryBuilder.GetSqlCommand();
            sqlCmd.Connection = _connection;
            _connection.Open();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Author author = MapDataReaderRowToAuthor(sqlReader);
                    authors.Add(author);
                }
            }
            _connection.Close();
            return authors;
        }

        public static Author GetById(Guid id)
        {
            Author author = null;
            SqlCommand sqlCmd = CreateSqlCommand("SELECT * FROM Author WHERE Id = @Id", ("@Id", id));
            _connection.Open();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                sqlReader.Read();
                author = MapDataReaderRowToAuthor(sqlReader);
            }
            _connection.Close();
            return author;
        }

        public static Author Update(Guid id, UpdateAuthorDto updateAuthorDto)
        {
            Author author = GetById(id);
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

            SqlCommand sqlCmd = CreateSqlCommand(
                "UPDATE Author SET Name = @Name, Gender = @Gender WHERE Id = @Id",
                ("@Id", id),
                ("@Name", author.Name),
                ("@Gender", author.Gender)
            );
            _connection.Open();
            sqlCmd.ExecuteNonQuery();
            _connection.Close();
            return author;
        }

        public static void Delete(Guid id)
        {
            SqlCommand sqlCmd = CreateSqlCommand("DELETE FROM Author WHERE Id = @Id", ("@Id", id));
            _connection.Open();
            int rowsAffected = sqlCmd.ExecuteNonQuery();
            _connection.Close();
            if (rowsAffected == 0)
            {
                throw new Exception();
            }
        }

        private static SqlCommand CreateSqlCommand(string sql, params (string Key, object Value)[] parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, _connection);
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            return cmd;
        }

        private static Author MapDataReaderRowToAuthor(SqlDataReader reader)
        {
            Author author = new Author();
            author.Id = reader.GetGuid(0);
            author.Name = reader.GetString(1);
            author.Gender = reader.GetString(2);
            return author;
        }

    }
}
