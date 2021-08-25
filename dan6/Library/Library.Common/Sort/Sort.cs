namespace Library.Common.Sort
{
    public class Sort : ISort
    {
#nullable enable
        public string? SortBy { get; set; }
        public string? Order { get; set; } = "ASC";
#nullable disable
    }
}
