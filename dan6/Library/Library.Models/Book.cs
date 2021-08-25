using Library.Model.Common;
using System;

namespace Library.Model
{
    public class Book : IBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public IAuthor Author { get; set; }
    }
}
