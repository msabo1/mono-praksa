using Library.Common.Filters;
using Library.Model.Common;

namespace Library.Service.Common
{
    public interface IAuthorsService : IBaseService<IAuthor, IAuthorFilter>
    {
    }
}
