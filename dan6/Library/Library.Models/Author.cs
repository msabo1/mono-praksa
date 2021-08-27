using Library.Model.Common;
using System;
using System.Collections.Generic;

namespace Library.Model
{
    public class Author : IAuthor, IBaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public ICollection<IBook> Books { get; set; } = new List<IBook>();
    }
}
