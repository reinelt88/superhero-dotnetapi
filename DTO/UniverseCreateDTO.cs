using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.DTO;

public class UniverseCreateDTO
{
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
    public string Name { get; set; }
}