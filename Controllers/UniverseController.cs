using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.DTO;
using SuperHeroAPI.Entity;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniverseController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UniverseController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //get all universes
        [HttpGet]
        public async Task<ActionResult<List<UniverseShowDTO>>> Get()
        {
            var universes = await _context.Universes.Include(universe => universe.SuperHeroes).ToListAsync();
            return _mapper.Map<List<UniverseShowDTO>>(universes);
        }

        //get a single universe by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UniverseShowDTO>> Get([FromRoute] int id)
        {
            var universe = await _context.Universes.Include(universe => universe.SuperHeroes).FirstOrDefaultAsync(universe => universe.Id == id);
            if (universe == null)
            {
                return NotFound();
            }
            return await Task.FromResult(_mapper.Map<UniverseShowDTO>(universe));
        }

        //add a universe
        [HttpPost]
        public async Task<ActionResult<Universe>> Post(UniverseCreateDTO universeCreateDto)
        {
            var universe = _mapper.Map<Universe>(universeCreateDto);
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
        public async Task<ActionResult<UniverseShowDTO>> Delete([FromRoute] int id)
        {
            var universe = await _context.Universes.FindAsync(id);
            if (universe == null)
            {
                return NotFound();
            }
            _context.Universes.Remove(universe);
            await _context.SaveChangesAsync();
            return _mapper.Map<UniverseShowDTO>(universe);
        }
        
        //get all universes by name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<List<UniverseShowDTO>>> GetByName([FromRoute] string name)
        {
            var universes = await _context.Universes.Where(u => u.Name.Contains(name)).ToListAsync();
            return _mapper.Map<List<UniverseShowDTO>>(universes);
        }
    }
}