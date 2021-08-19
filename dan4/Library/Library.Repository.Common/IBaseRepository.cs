using Library.Model.Common;
using System;
using System.Collections.Generic;

namespace Library.Repository.Common
{
    public interface IBaseRepository<ModelT, CreateDtoT, UpdateDtoT, QueryDtoT> where QueryDtoT : IBaseQuery
    {
        ModelT Create(CreateDtoT createDto);
        ICollection<ModelT> Get(QueryDtoT queryDto);
        ModelT GetById(Guid id);
        ModelT Update(Guid id, UpdateDtoT updateAuthorDto);
        void Delete(Guid id);
    }
}
