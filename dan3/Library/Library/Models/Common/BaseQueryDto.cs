namespace Day2.Models.Common
{
    public abstract class BaseQueryDto
    {
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string? Order { get; set; }
    }
}
