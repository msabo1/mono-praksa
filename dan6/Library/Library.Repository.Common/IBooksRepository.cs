using Library.Common.Filters;
using Library.Model.Common;

namespace Library.Repository.Common
{
    public interface IBooksRepository : IBaseRepository<IBook, IBookFilter>
    {
    }
}
