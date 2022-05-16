using System.ComponentModel.DataAnnotations;
using SuperHeroAPI.Validations;

namespace SuperHeroAPI.DTO;

public class SuperHeroCreateDTO
{
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength:30, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
    [FirstLetterUpper]
    public string Name { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength:30, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength:30, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
    [FirstLetterUpper]
    public string LastName { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength:30, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
    [FirstLetterUpper]
    public string Place { get; set; }
    public int UniverseId { get; set; }
}