using Library.Common.Pagination;
using Library.Common.Sort;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Repository.Common
{
    public interface IBaseRepository<ModelT, FilterT>
    {
        Task<ModelT> CreateAsync(ModelT model);
        Task<ICollection<ModelT>> GetAsync(ISort sort = null, IPagination pagination = null, FilterT filter = default);
        Task<ModelT> GetByIdAsync(Guid id);
        Task<ModelT> UpdateAsync(ModelT model);
        Task DeleteAsync(Guid id);
    }
}
