using System.ComponentModel.DataAnnotations;
using SuperHeroAPI.Entity;
using SuperHeroAPI.Validations;

namespace SuperHeroAPI.DTO;

public class MovieCreateDTO
{
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength:30, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
    [FirstLetterUpper]
    public string Name { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength:4, ErrorMessage = "The maximum length is {1} characters")]
    public string year { get; set; }
    public List<int> SuperHeroesId { get; set; }
}