using Day2.Models.Book;
using Library.Models.Author;
using Library.Models.Book;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Day2.Repositories
{
    public class BooksRepository
    {
        private static SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
        public static Book Create(CreateBookDto createBookDto)
        {
            Guid id = Guid.NewGuid();
            Book book = new Book();
            book.Id = id;
            book.Name = createBookDto.Name;
            book.AuthorId = createBookDto.AuthorId;
            SqlCommand sqlCmd = CreateSqlCommand(
                "INSERT INTO Book VALUES(@Id, @Name, @AuthorId)",
                ("@Id", book.Id),
                ("@Name", book.Name),
                ("@AuthorId", book.AuthorId)
            );
            _connection.Open();
            sqlCmd.ExecuteNonQuery();
            _connection.Close();
            return book;
        }

        public static ICollection<Book> Get(QueryBooksDto queryBooksDto = null)
        {
            ICollection<Book> books = new List<Book>();
            QueryBuilder queryBuilder = new QueryBuilder();
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

            SqlCommand sqlCmd = queryBuilder.GetSqlCommand();
            sqlCmd.Connection = _connection;
            _connection.Open();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Book book = MapDataReaderRowToBook(sqlReader);
                    books.Add(book);
                }
            }
            _connection.Close();
            return books;
        }

        public static Book GetById(Guid id)
        {
            Book book = null;
            SqlCommand sqlCmd = CreateSqlCommand("SELECT * FROM Book WHERE Id = @Id", ("@Id", id));
            _connection.Open();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                sqlReader.Read();
                book = MapDataReaderRowToBook(sqlReader);
            }
            _connection.Close();
            return book;
        }

        public static Book Update(Guid id, UpdateBookDto updateBookDto)
        {
            Book book = GetById(id);
            if (book == null)
            {
                return null;
            }

            if (updateBookDto.Name != null)
            {
                book.Name = updateBookDto.Name;
            }
            if (updateBookDto.AuthorId != null)
            {
                book.AuthorId = (Guid)updateBookDto.AuthorId;
            }

            SqlCommand sqlCmd = CreateSqlCommand(
                "UPDATE Book SET Name = @Name, AuthorId = @AuthorId WHERE Id = @Id",
                ("@Id", id),
                ("@Name", book.Name),
                ("@AuthorId", book.AuthorId)
            );
            _connection.Open();
            sqlCmd.ExecuteNonQuery();
            _connection.Close();
            return book;
        }

        public static void Delete(Guid id)
        {
            SqlCommand sqlCmd = CreateSqlCommand("DELETE FROM Book WHERE Id = @Id", ("@Id", id));
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

        private static Book MapDataReaderRowToBook(SqlDataReader reader)
        {
            Book book = new Book();
            book.Id = reader.GetGuid(0);
            book.Name = reader.GetString(1);
            book.AuthorId = reader.GetGuid(2);
            book.Author = new Author();
            book.Author.Id = reader.GetGuid(3);
            book.Author.Name = reader.GetString(4);
            book.Author.Gender = reader.GetString(5);
            return book;
        }
    }
}
