namespace Library.Model.Common
{
    public class BaseQueryDto : IBaseQuery
    {
#nullable enable
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string? Order { get; set; }
#nullable disable
    }
}
