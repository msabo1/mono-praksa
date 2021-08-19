using Library.Model.Common;
using Library.Model.Common.Author;
using Library.Repository;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;

namespace Library.Service
{
    public class AuthorsService : IAuthorsService
    {
        private IAuthorsRepository _repository = new AuthorsRepository();
        public IAuthor Create(ICreateAuthorDto createDto)
        {
            return _repository.Create(createDto);
        }


        public ICollection<IAuthor> Get(IQueryAuthorsDto queryDto)
        {
            return _repository.Get(queryDto);
        }

        public IAuthor GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IAuthor Update(Guid id, IUpdateAuthorDto updateAuthorDto)
        {
            return _repository.Update(id, updateAuthorDto);
        }

        public bool Delete(Guid id)
        {
            try
            {
                _repository.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
