using Library.Model.Common;
using System;

namespace Library.Model.Author
{
    public class Author : IAuthor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}
