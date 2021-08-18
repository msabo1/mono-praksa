using System;

namespace Library.Models.Book
{
    public class UpdateBookDto
    {
        public string? Name { get; set; }
        public Guid? AuthorId { get; set; }
    }
}
