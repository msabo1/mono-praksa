using System;

namespace Library.Model.Common
{
    public interface IBook : IBaseModel
    {
        Guid Id { get; set; }
        string Title { get; set; }
        Guid AuthorId { get; set; }
        IAuthor Author { get; set; }
    }
}
