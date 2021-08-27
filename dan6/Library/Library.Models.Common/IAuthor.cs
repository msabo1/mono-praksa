using System;
using System.Collections.Generic;

namespace Library.Model.Common
{
    public interface IAuthor : IBaseModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Gender { get; set; }
        ICollection<IBook> Books { get; set; }
    }
}
