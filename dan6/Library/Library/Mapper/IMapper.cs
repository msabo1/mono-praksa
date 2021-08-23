using Library.Controllers;
using Library.Model.Common;
using System.Collections.Generic;

namespace Library
{
    public interface IMapper
    {
        ICollection<AuthorRest> CollectionMapAuthorDomainToRest(ICollection<IAuthor> authors);
        ICollection<BookRest> CollectionMapBookDomainToRest(ICollection<IBook> books);
        AuthorRest MapAuthorDomainToRest(IAuthor author);
        BookRest MapBookDomainToRest(IBook book);
    }
}