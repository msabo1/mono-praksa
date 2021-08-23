using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service.Common
{
    public interface IBaseService<ModelT, QueryDtoT>
    {
        Task<ModelT> CreateAsync(ModelT model);
        Task<ICollection<ModelT>> GetAsync(QueryDtoT queryDto);
        Task<ModelT> GetByIdAsync(Guid id);
        Task<ModelT> UpdateAsync(ModelT model);
        Task<bool> DeleteAsync(Guid id);
    }
}
