using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entity;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>{
            new SuperHero { Id = 1, Name = "Superman", FirstName = "Clark", LastName = "Kent", Place = "Metropolis" },
            new SuperHero { Id = 2, Name = "Batman", FirstName = "Bruce", LastName = "Wayne", Place = "Gotham City" },
            new SuperHero { Id = 3, Name = "Spiderman", FirstName = "Peter", LastName = "Parker", Place = "New York" },
            new SuperHero { Id = 4, Name = "Ironman", FirstName = "Tony", LastName = "Stark", Place = "New York" },
            new SuperHero { Id = 5, Name = "Captain America", FirstName = "Steve", LastName = "Rogers", Place = "New York" },
            new SuperHero { Id = 6, Name = "Wolverine", FirstName = "James", LastName = "Howlett", Place = "New York" },
            new SuperHero { Id = 7, Name = "Hulk", FirstName = "Bruce", LastName = "Banner", Place = "New York" },
        };

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return await _context.SuperHeroes.ToListAsync();
        }

        // Add a super hero 
        [HttpPost]
        public async Task<ActionResult<SuperHero>> Post(SuperHero hero)
        {
            var universe = await _context.Universes.FindAsync(hero.UniverseId);
            if (universe == null)
            {
                return NotFound($"Universe {hero.UniverseId} does not exist");
            }
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return hero;
        }

        // Get a single hero by id
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.Include(h => h.Universe).FirstOrDefaultAsync(h => h.Id == id);
            if (hero == null)
            {
                return NotFound();
            }
            return await Task.FromResult(hero);
        }

        // Update a hero
        [HttpPut("{id}")]
        public async Task<ActionResult<SuperHero>> Put(int id, SuperHero hero)
        {
            var existingHero = await _context.SuperHeroes.FindAsync(id);
            if (existingHero != null)
            {
                // verify if the universe exists
                var universe = await _context.Universes.FindAsync(hero.UniverseId);
                if (universe == null)
                {
                    return NotFound($"Universe {hero.UniverseId} does not exist");
                }
                existingHero.Name = hero.Name;
                existingHero.FirstName = hero.FirstName;
                existingHero.LastName = hero.LastName;
                existingHero.Place = hero.Place;
                existingHero.FirstName = hero.FirstName;

                _context.SuperHeroes.Update(existingHero);
                await _context.SaveChangesAsync();
                return existingHero;
            }
            return await Task.FromResult(hero);
        }

        // Delete a single hero by id
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            return hero;
        }
    }
}