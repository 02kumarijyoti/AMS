using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; } 
        [Required]
        public string Status { get; set; }
    }
}