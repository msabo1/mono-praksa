using Library.Model.Common;
using Library.Model.Common.Book;

namespace Library.Repository.Common
{
    public interface IBooksRepository : IBaseRepository<IBook, IQueryBooksDto>
    {
    }
}
