using System;

namespace Library.Model.Common
{
    public interface IAuthor
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Gender { get; set; }
    }
}
