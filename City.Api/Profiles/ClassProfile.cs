using AutoMapper;

namespace City.Api.Profiles
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Entity.Citie, Model.CityDtoWithOutPointOfInterest>();
            CreateMap<Entity.Citie, Model.CitiesDto>();

        }
    }
}
