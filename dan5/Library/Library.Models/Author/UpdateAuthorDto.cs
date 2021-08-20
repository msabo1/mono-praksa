using Library.Model.Common.Author;

namespace Library.Model.Author
{
    public class UpdateAuthorDto : IUpdateAuthorDto
    {
#nullable enable
        public string? Name { get; set; }
        public string? Gender { get; set; }
#nullable disable
    }
}
