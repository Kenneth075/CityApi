using AutoMapper;

namespace City.Api.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entity.PointsOfInterest, Model.PointOfInterestDto>();

            CreateMap<Model.PointOfInterestForCreationDto, Entity.PointsOfInterest>();

            CreateMap<Model.PointOfInterestForUpdateDto, Entity.PointsOfInterest>();

            CreateMap<Entity.PointsOfInterest, Model.PointOfInterestForUpdateDto>();

            
        }
    }
}
