using System;

namespace Library.Model.Common.Book
{
    public interface IUpdateBookDto
    {
#nullable enable
        string? Name { get; set; }
        Guid? AuthorId { get; set; }
#nullable disable
    }
}
