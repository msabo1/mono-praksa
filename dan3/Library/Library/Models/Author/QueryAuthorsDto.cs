using Day2.Models.Common;

namespace Day2.Models.Author
{
    public class QueryAuthorsDto : BaseQueryDto
    {
        public string? Gender { get; set; }
    }
}
