using Library.Controllers;
using Library.Model.Common;
using System.Collections.Generic;

namespace Library
{
    public class Mapper
    {
        public AuthorRest MapAuthorDomainToRest(IAuthor author)
        {
            AuthorRest authorRest = new AuthorRest();
            authorRest.Id = author.Id;
            authorRest.Gender = author.Gender;
            authorRest.Name = author.Name;
            return authorRest;
        }

        public ICollection<AuthorRest> CollectionMapAuthorDomainToRest(ICollection<IAuthor> authors)
        {
            ICollection<AuthorRest> authorRests = new List<AuthorRest>();
            foreach (IAuthor author in authors)
            {
                authorRests.Add(MapAuthorDomainToRest(author));
            }
            return authorRests;
        }

        public BookRest MapBookDomainToRest(IBook book)
        {
            BookRest bookRest = new BookRest();
            bookRest.Id = book.Id;
            bookRest.Name = book.Name;
            bookRest.Author = MapAuthorDomainToRest(book.Author);
            return bookRest;
        }

        public ICollection<BookRest> CollectionMapBookDomainToRest(ICollection<IBook> books)
        {
            ICollection<BookRest> bookRests = new List<BookRest>();
            foreach (IBook book in books)
            {
                bookRests.Add(MapBookDomainToRest(book));
            }
            return bookRests;
        }
    }
}
