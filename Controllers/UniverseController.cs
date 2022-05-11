using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Entity;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniverseController : ControllerBase
    {
        private readonly DataContext _context;

        public UniverseController(DataContext context)
        {
            _context = context;
        }

        //get all universes
        [HttpGet]
        public async Task<ActionResult<List<Universe>>> Get()
        {
            return await _context.Universes.Include(u => u.SuperHeroes).ToListAsync();
        }

        //get a single universe by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Universe>> Get([FromRoute] int id)
        {
            var universe = await _context.Universes.FindAsync(id);
            if (universe == null)
            {
                return NotFound();
            }
            return await Task.FromResult(universe);
        }

        //add a universe
        [HttpPost]
        public async Task<ActionResult<Universe>> Post(Universe universe)
        {
            _context.Universes.Add(universe);
            await _context.SaveChangesAsync();
            return universe;
        }

        //update a universe
        [HttpPut("{id}")]
        public async Task<ActionResult<Universe>> Put([FromRoute] int id, [FromBody] Universe universe)
        {
            if (id != universe.Id)
            {
                return BadRequest();
            }
            _context.Entry(universe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return universe;
        }

        //delete a universe
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Universe>> Delete([FromRoute] int id)
        {
            var universe = await _context.Universes.FindAsync(id);
            if (universe == null)
            {
                return NotFound();
            }
            _context.Universes.Remove(universe);
            await _context.SaveChangesAsync();
            return universe;
        }
        
        //get all universes by name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<List<Universe>>> GetByName([FromRoute] string name)
        {
            return await _context.Universes.Where(u => u.Name.Contains(name)).ToListAsync();
        }
    }
}