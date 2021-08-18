using System;

namespace Day2.Models.Book
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AuthorId { get; set; }
        public Library.Models.Author.Author Author { get; set; }
    }
}
