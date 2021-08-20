using Library.Model.Common;
using Library.Model.Common.Author;
using Library.Repository;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public class AuthorsService : IAuthorsService
    {
        private IAuthorsRepository _repository = new AuthorsRepository();
        public async Task<IAuthor> CreateAsync(ICreateAuthorDto createDto)
        {
            return await _repository.CreateAsync(createDto);
        }


        public async Task<ICollection<IAuthor>> GetAsync(IQueryAuthorsDto queryDto)
        {
            return await _repository.GetAsync(queryDto);
        }

        public async Task<IAuthor> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IAuthor> UpdateAsync(Guid id, IUpdateAuthorDto updateAuthorDto)
        {
            return await _repository.UpdateAsync(id, updateAuthorDto);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
