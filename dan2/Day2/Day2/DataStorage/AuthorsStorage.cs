using Day2.Models.Author;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day2.DataStorage
{
    public class AuthorsStorage
    {
        private static ICollection<Author> _authors = new List<Author>();

        public static Author Create(CreateAuthorDto createAuthorDto)
        {
            Guid id = Guid.NewGuid();
            Author author = new Author();
            author.Id = id;
            author.Name = createAuthorDto.Name;
            author.Gender = createAuthorDto.Gender;
            _authors.Add(author);
            return author;
        }

        public static ICollection<Author> GetAll()
        {
            return _authors;
        }

        public static Author GetById(Guid id)
        {
            return _authors.Where(a => a.Id == id).FirstOrDefault();

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
            return author;
        }

        public static void Delete(Guid id)
        {
            _authors.Remove(_authors.Single(a => a.Id == id));
        }

    }
}
