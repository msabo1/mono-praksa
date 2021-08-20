using Library.Model.Common.Book;
using System;

namespace Library.Model.Book
{
    public class UpdateBookDto : IUpdateBookDto
    {
#nullable enable
        public string? Name { get; set; }
        public Guid? AuthorId { get; set; }
#nullable disable

    }
}
