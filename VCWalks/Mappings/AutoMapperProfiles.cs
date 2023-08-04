using AutoMapper;
using VCWalks.Models.Domain;
using VCWalks.Models.DTO;

namespace VCWalks.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
           CreateMap<AddWalkRequestDTO, Walk>().ReverseMap();
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();




        }
    }
}
