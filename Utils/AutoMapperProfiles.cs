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
            CreateMap<SuperHero, SuperHeroShowDTO>();
        }
    }
}