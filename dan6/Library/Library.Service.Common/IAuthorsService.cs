using Library.Model.Common;
using Library.Model.Common.Author;

namespace Library.Service.Common
{
    public interface IAuthorsService : IBaseService<IAuthor, IQueryAuthorsDto>
    {
    }
}
