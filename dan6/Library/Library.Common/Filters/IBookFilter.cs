using System;

namespace Library.Common.Filters
{
    public interface IBookFilter : IBaseFilter
    {
#nullable enable
        Guid? AuthorId { get; set; }
#nullable disable
    }
}
