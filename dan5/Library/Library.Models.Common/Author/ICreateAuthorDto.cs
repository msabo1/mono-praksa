using System.ComponentModel.DataAnnotations;

namespace Library.Model.Common.Author
{
    public interface ICreateAuthorDto
    {
        [Required]
        string Name { get; set; }
        [Required]
        string Gender { get; set; }
    }
}
