using Library.Common.Filters;
using Library.Common.Pagination;
using Library.Common.Sort;
using Library.Model.Common;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public class AuthorsService : IAuthorsService
    {
        private IAuthorsRepository _repository;

        public AuthorsService(IAuthorsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IAuthor> CreateAsync(IAuthor author)
        {
            return await _repository.CreateAsync(author);
        }


        public async Task<ICollection<IAuthor>> GetAsync(ISort sort = null, IPagination pagination = null, IAuthorFilter filter = null)
        {
            return await _repository.GetAsync(sort, pagination, filter);
        }

        public async Task<IAuthor> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IAuthor> UpdateAsync(IAuthor author)
        {
            return await _repository.UpdateAsync(author);
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
