namespace Library.Common.Pagination
{
    public interface IPagination
    {
#nullable enable
        int? PageNumber { get; set; }
        int? PageSize { get; set; }
#nullable disable
    }
}
