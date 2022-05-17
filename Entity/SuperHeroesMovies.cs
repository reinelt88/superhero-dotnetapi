namespace SuperHeroAPI.Entity;

public class SuperHeroesMovies
{
    public int SuperHeroId { get; set; }
    public int MovieId { get; set; }
    public SuperHero SuperHero { get; set; }
    public Movie Movie { get; set; }
}