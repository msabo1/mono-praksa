using Library.Model.Common.Book;
using System;

namespace Library.Model.Book
{
    public class CreateBookDto : ICreateBookDto
    {
        public string Name { get; set; }
        public Guid AuthorId { get; set; }
    }
}
