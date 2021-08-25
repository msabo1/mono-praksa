namespace Library.Common.Filters
{
    public interface IAuthorFilter : IBaseFilter
    {
#nullable enable
        string? Gender { get; set; }
#nullable disable
    }
}
