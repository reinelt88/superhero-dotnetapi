using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
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
            return await _context.Universes.ToListAsync();
        }

        //get a single universe by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Universe>> Get(int id)
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
        public async Task<ActionResult<Universe>> Put(int id, Universe universe)
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<Universe>> Delete(int id)
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
    }
}