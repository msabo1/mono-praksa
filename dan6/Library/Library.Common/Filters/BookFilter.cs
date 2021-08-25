using System;

namespace Library.Common.Filters
{
    public class BookFilter : BaseFilter, IBookFilter
    {
#nullable enable
        public Guid? AuthorId { get; set; }
#nullable disable
    }
}
