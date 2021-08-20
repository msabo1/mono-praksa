using Library.Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Repository.Common
{
    public interface IBaseRepository<ModelT, QueryDtoT> where QueryDtoT : IBaseQuery
    {
        Task<ModelT> CreateAsync(ModelT model);
        Task<ICollection<ModelT>> GetAsync(QueryDtoT queryDto);
        Task<ModelT> GetByIdAsync(Guid id);
        Task<ModelT> UpdateAsync(ModelT model);
        Task DeleteAsync(Guid id);
    }
}
