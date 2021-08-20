using Library.Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Repository.Common
{
    public interface IBaseRepository<ModelT, CreateDtoT, UpdateDtoT, QueryDtoT> where QueryDtoT : IBaseQuery
    {
        Task<ModelT> CreateAsync(CreateDtoT createDto);
        Task<ICollection<ModelT>> GetAsync(QueryDtoT queryDto);
        Task<ModelT> GetByIdAsync(Guid id);
        Task<ModelT> UpdateAsync(Guid id, UpdateDtoT updateAuthorDto);
        Task DeleteAsync(Guid id);
    }
}
