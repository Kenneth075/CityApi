using City.Api.Entity;

namespace City.Api.Services
{
    public interface ICityRepository
    {
        Task<IEnumerable<Citie>> GetCitiesAsync();
        Task<(IEnumerable<Citie>, PaginationMetaData)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Citie?> GetCityAsync(int cityId, bool includePointOfInterest);
        Task<bool> CityExistAsync(int cityId);
        Task<IEnumerable<PointsOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointsOfInterest> GetPointOfInterestsForCityAsync(int cityId, int pointsOfInterestId);
        Task CreatePointOfInterestForCityAsync(int cityId, PointsOfInterest pointOfInterest);
        void DeletePointOfInterestForCity(PointsOfInterest pointOfInterest);
        //Task<bool> CityNameMatchesCityId(string? cityName, int cityId);
        Task<bool> SaveChangesAsync();
    }
}
