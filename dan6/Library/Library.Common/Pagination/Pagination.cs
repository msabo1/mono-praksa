namespace Library.Common.Pagination
{
    public class Pagination : IPagination
    {
#nullable enable
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; } = 10;
#nullable disable
    }
}
