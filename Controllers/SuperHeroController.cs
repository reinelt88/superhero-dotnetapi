using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.DTO;
using SuperHeroAPI.Entity;
using SuperHeroAPI.Filters;

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
            new SuperHero { Id = 8, Name = "Thor", FirstName = "Thor", LastName = "Odinson", Place = "Asgard" },
            new SuperHero { Id = 9, Name = "Black Widow", FirstName = "Natasha", LastName = "Romanova", Place = "New York" },
            new SuperHero { Id = 10, Name = "Vision", FirstName = "Vision", LastName = "Vision", Place = "New York" },
        };

        private readonly DataContext _context;
        private readonly ILogger<SuperHeroController> _logger;
        private readonly IMapper _mapper;

        public SuperHeroController(DataContext context, ILogger<SuperHeroController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ResponseCache(Duration = 10)]
        public async Task<ActionResult<List<SuperHeroShowDTO>>> Get()
        {
            _logger.LogInformation("Get all heroes");
            var superHeroes = await _context.SuperHeroes.Include(superhero => superhero.Universe).ToListAsync();
            return _mapper.Map<List<SuperHeroShowDTO>>(superHeroes);
        }

        // Add a super hero 
        [HttpPost]
        public async Task<ActionResult<SuperHeroShowDTO>> Post([FromBody] SuperHeroCreateDTO superHeroCreateDto)
        {
            var universe = await _context.Universes.FindAsync(superHeroCreateDto.UniverseId);
            if (universe == null)
            {
                return NotFound($"Universe {superHeroCreateDto.UniverseId} does not exist");
            }
            
            //verify if exits a hero with the same name
            var existingHero = await _context.SuperHeroes.FirstOrDefaultAsync(h => h.Name == superHeroCreateDto.Name);
            if (existingHero != null)
            {
                return BadRequest($"Hero {superHeroCreateDto.Name} already exists");
            }

            var hero = _mapper.Map<SuperHero>(superHeroCreateDto);
            
            _context.SuperHeroes.Add(hero);
            
            await _context.SaveChangesAsync();
            
            return _mapper.Map<SuperHeroShowDTO>(hero);
        }

        // Get a single hero by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SuperHeroShowDTO>> Get([FromRoute] int id)
        {
            var hero = await _context.SuperHeroes.Include(superhero => superhero.Universe).FirstOrDefaultAsync(superhero => superhero.Id == id);
            if (hero == null)
            {
                return NotFound();
            }
            var superHeroDto = _mapper.Map<SuperHeroShowDTO>(hero);
            return await Task.FromResult(superHeroDto);
        }

        // Update a hero
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SuperHero>> Put([FromRoute] int id, [FromBody] SuperHero hero)
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
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult<SuperHeroShowDTO>> Delete([FromRoute] int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            return _mapper.Map<SuperHeroShowDTO>(hero);
        }
        
        // Get all heroes from a universe
        [HttpGet("universe/{id:int}")]
        public async Task<ActionResult<List<SuperHeroShowDTO>>> GetByUniverse([FromRoute] int id)
        {
            var heroes = await _context.SuperHeroes.Where(h => h.UniverseId == id).Include(superhero => superhero.Universe).ToListAsync();
            // return not found if heroes is empty
            if (heroes.Count == 0)
            {
                return NotFound();
            }
            return await Task.FromResult(_mapper.Map<List<SuperHeroShowDTO>>(heroes));
        }
    }
}