using Library.Model.Common;
using System;

namespace Library.Model.Book
{
    public class Book : IBook
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AuthorId { get; set; }
        public IAuthor Author { get; set; }
    }
}
