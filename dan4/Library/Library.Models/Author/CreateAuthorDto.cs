using Library.Model.Common.Author;

namespace Library.Model.Author
{
    public class CreateAuthorDto : ICreateAuthorDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}
