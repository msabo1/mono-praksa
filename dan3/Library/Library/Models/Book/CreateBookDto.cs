using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Book
{
    public class CreateBookDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
    }
}
