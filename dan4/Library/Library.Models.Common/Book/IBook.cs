using System;

namespace Library.Model.Common
{
    public interface IBook
    {
        Guid Id { get; set; }
        string Name { get; set; }
        Guid AuthorId { get; set; }
        IAuthor Author { get; set; }
    }
}
