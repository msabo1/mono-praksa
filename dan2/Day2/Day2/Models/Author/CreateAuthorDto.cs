using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Day2.Models.Author
{
    public class CreateAuthorDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
