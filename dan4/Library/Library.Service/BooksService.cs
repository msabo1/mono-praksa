using Library.Model.Common;
using Library.Model.Common.Book;
using Library.Repository;
using Library.Repository.Common;
using Library.Service.Common;
using System;
using System.Collections.Generic;

namespace Library.Service
{
    public class BooksService : IBooksService
    {
        private IBooksRepository _repository = new BooksRepository();
        public IBook Create(ICreateBookDto createDto)
        {
            return _repository.Create(createDto);
        }


        public ICollection<IBook> Get(IQueryBooksDto queryDto)
        {
            return _repository.Get(queryDto);
        }

        public IBook GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IBook Update(Guid id, IUpdateBookDto updateBookDto)
        {
            return _repository.Update(id, updateBookDto);
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
