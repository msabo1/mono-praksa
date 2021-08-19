using Library.Model.Common;
using Library.Model.Common.Book;

namespace Library.Service.Common
{
    public interface IBooksService : IBaseService<IBook, ICreateBookDto, IUpdateBookDto, IQueryBooksDto>
    {
    }
}
