namespace SuperHeroAPI.Entity
{
    public class Universe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<SuperHero> SuperHeroes { get; set; }
    }
}