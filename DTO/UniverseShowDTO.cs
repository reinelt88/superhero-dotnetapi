namespace SuperHeroAPI.DTO;

public class UniverseShowDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<SuperHeroShowDTO> SuperHeroes { get; set; }
}