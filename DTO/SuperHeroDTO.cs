using SuperHeroAPI.Entity;

namespace SuperHeroAPI.DTO;

public class SuperHeroDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Place { get; set; }
    public UniverseDTO Universe { get; set; }
}