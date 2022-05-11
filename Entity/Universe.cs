using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Entity
{
    public class Universe : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Name { get; set; }

        public List<SuperHero> SuperHeroes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name != "Marvel" || Name != "DC")
            {
                yield return new ValidationResult("Universe must be Marvel or DC", new[] { "Name" });
            }
        }
    }
}