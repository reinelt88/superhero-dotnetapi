namespace SuperHeroAPI.DTO;

public class UniverseDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<SuperHeroDTO> SuperHeroes { get; set; }
}