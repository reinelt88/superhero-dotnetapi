using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Entity;

namespace SuperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public DataContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration.GetConnectionString("WebApiDatabase");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //create many to many relationship between SuperHero and Movie
            modelBuilder.Entity<SuperHeroesMovies>()
                .HasKey(sc => new { sc.SuperHeroId, sc.MovieId });
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<Universe> Universes { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<SuperHeroesMovies> SuperHeroesMovies { get; set; }
        
    }
}