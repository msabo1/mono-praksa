using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service.Common
{
    public interface IBaseService<ModelT, CreateDtoT, UpdateDtoT, QueryDtoT>
    {
        Task<ModelT> CreateAsync(CreateDtoT createDto);
        Task<ICollection<ModelT>> GetAsync(QueryDtoT queryDto);
        Task<ModelT> GetByIdAsync(Guid id);
        Task<ModelT> UpdateAsync(Guid id, UpdateDtoT updateAuthorDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
