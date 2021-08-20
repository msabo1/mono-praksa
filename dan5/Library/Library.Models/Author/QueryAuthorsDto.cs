using Library.Model.Common;
using Library.Model.Common.Author;

namespace Library.Model.Author
{
    public class QueryAuthorsDto : BaseQueryDto, IQueryAuthorsDto
    {
#nullable enable
        public string? Gender { get; set; }
#nullable disable
    }
}
