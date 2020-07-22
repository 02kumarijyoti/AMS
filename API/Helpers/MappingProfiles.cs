using API.Dtos;
using API.Entintes;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ResourceDto,Resource>();
            CreateMap<ProjectDto,Project>();
        }
    }
}