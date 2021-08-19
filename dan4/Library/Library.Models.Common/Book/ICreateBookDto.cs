using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Model.Common.Book
{
    public interface ICreateBookDto
    {
        [Required]
        string Name { get; set; }
        [Required]
        Guid AuthorId { get; set; }
    }
}
