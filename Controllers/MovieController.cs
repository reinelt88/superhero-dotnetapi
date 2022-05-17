using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.DTO;
using SuperHeroAPI.Entity;

namespace SuperHeroAPI.Controllers;

public class MovieController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public MovieController(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    //get all movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
    {
        var movies = await _context.Movie.ToListAsync();
        return _mapper.Map<List<MovieDTO>>(movies);
    }
    
    //get movie by id
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDTO>> GetMovie(int id)
    {
        var movie = await _context.Movie.FirstOrDefaultAsync(x => x.Id == id);
        if (movie == null)
        {
            return NotFound();
        }
        return _mapper.Map<MovieDTO>(movie);
    }
    
    //create movie
    [HttpPost]
    public async Task<ActionResult<MovieDTO>> CreateMovie(MovieCreateDTO movie)
    {
        if (movie.SuperHeroesId == null)
        {
            return BadRequest("SuperheroesId is required");
        }     
        
        var heroesId = await _context.SuperHeroes.Where(hero => movie.SuperHeroesId.Contains(hero.Id)).Select(hero => hero.Id).ToListAsync();
        if (heroesId.Count != movie.SuperHeroesId.Count)
        {
            return BadRequest("One or more super heroes does not exist");
        }
        var movieEntity = _mapper.Map<Movie>(movie);
        _context.Movie.Add(movieEntity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMovie), new { id = movieEntity.Id }, movie);
    }
    
    //update movie
    [HttpPut("{id}")]
    public async Task<ActionResult<MovieDTO>> UpdateMovie(int id, MovieCreateDTO movie)
    {
        var movieFromDb = await _context.Movie.FirstOrDefaultAsync(x => x.Id == id);
        if (movieFromDb == null)
        {
            return NotFound();
        }
        _mapper.Map(movie, movieFromDb);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    //delete movie
    [HttpDelete("{id}")]
    public async Task<ActionResult<MovieDTO>> DeleteMovie(int id)
    {
        var movieFromDb = await _context.Movie.FirstOrDefaultAsync(x => x.Id == id);
        if (movieFromDb == null)
        {
            return NotFound();
        }
        _context.Movie.Remove(movieFromDb);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
}