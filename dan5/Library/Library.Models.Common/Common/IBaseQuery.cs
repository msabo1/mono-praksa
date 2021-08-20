namespace Library.Model.Common
{
    public interface IBaseQuery
    {
#nullable enable
        string? Search { get; set; }
        string? SortBy { get; set; }
        string? Order { get; set; }
#nullable disable
    }
}
