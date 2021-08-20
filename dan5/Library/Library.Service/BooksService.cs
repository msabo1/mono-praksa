using Library.Model.Common;
using Library.Model.Common.Book;
using Library.Repository;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public class BooksService : IBooksService
    {
        private IBooksRepository _repository = new BooksRepository();
        public async Task<IBook> CreateAsync(ICreateBookDto createDto)
        {
            return await _repository.CreateAsync(createDto);
        }


        public async Task<ICollection<IBook>> GetAsync(IQueryBooksDto queryDto)
        {
            return await _repository.GetAsync(queryDto);
        }

        public async Task<IBook> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IBook> UpdateAsync(Guid id, IUpdateBookDto updateBookDto)
        {
            return await _repository.UpdateAsync(id, updateBookDto);
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
