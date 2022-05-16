using SuperHeroAPI.Entity;

namespace SuperHeroAPI.DTO;

public class SuperHeroShowDTO
{
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Place { get; set; }
    public Universe Universe { get; set; }
}