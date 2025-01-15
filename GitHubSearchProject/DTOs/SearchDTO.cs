using System.ComponentModel.DataAnnotations;

namespace GitHubSearchProject.DTOs
{
    public record SearchDTO(
        [Required] string KeyWord
    );


}
