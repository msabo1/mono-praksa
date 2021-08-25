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
    public class BooksService : IBooksService
    {
        private IBooksRepository _repository;

        public BooksService(IBooksRepository repository)
        {
            _repository = repository;
        }

        public async Task<IBook> CreateAsync(IBook book)
        {
            return await _repository.CreateAsync(book);
        }


        public async Task<ICollection<IBook>> GetAsync(ISort sort = null, IPagination pagination = null, IBookFilter filter = null)
        {
            return await _repository.GetAsync(sort, pagination, filter);
        }

        public async Task<IBook> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IBook> UpdateAsync(IBook book)
        {
            return await _repository.UpdateAsync(book);
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
