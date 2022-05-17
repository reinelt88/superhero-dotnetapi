using AutoMapper;
using SuperHeroAPI.DTO;
using SuperHeroAPI.Entity;

namespace SuperHeroAPI.Utils;

public class AutoMapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SuperHeroCreateDTO, SuperHero>();
            CreateMap<SuperHero, SuperHeroDTO>();
            CreateMap<UniverseCreateDTO, Universe>();
            CreateMap<Universe, UniverseDTO>();
            CreateMap<MovieCreateDTO, Movie>().ForMember(movie => movie.SuperHeroesMovies, opt => opt.Ignore());
            CreateMap<Movie, MovieCreateDTO>();
        }

        private List<SuperHeroesMovies> mapSuperHeroesMovies(MovieCreateDTO movieCreateDto, Movie movie)
        {
            var result = new List<SuperHeroesMovies>();
            if (movieCreateDto.SuperHeroesId == null)
            {
                return result;
            }
    
            foreach (var superHeroId in movieCreateDto.SuperHeroesId)
            {
                result.Add(new SuperHeroesMovies()
                {
                    MovieId = movie.Id,
                    SuperHeroId = superHeroId
                });
            }
            return result;
        }
    }
}