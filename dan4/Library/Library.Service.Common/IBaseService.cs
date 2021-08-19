using System;
using System.Collections.Generic;

namespace Library.Service.Common
{
    public interface IBaseService<ModelT, CreateDtoT, UpdateDtoT, QueryDtoT>
    {
        ModelT Create(CreateDtoT createDto);
        ICollection<ModelT> Get(QueryDtoT queryDto);
        ModelT GetById(Guid id);
        ModelT Update(Guid id, UpdateDtoT updateAuthorDto);
        bool Delete(Guid id);
    }
}
