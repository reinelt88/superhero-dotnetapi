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

        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<Universe> Universes { get; set; }
        
    }
}