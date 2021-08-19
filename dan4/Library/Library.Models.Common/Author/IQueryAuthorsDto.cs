namespace Library.Model.Common.Author
{
    public interface IQueryAuthorsDto : IBaseQuery
    {
#nullable enable
        string? Gender { get; set; }
#nullable disable
    }
}
