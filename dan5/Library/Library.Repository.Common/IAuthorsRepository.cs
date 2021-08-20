using Library.Model.Common;
using Library.Model.Common.Author;

namespace Library.Repository.Common
{
    public interface IAuthorsRepository : IBaseRepository<IAuthor, ICreateAuthorDto, IUpdateAuthorDto, IQueryAuthorsDto>
    {

    }
}
