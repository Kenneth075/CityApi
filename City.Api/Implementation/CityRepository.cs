using City.Api.DbContextCity;
using City.Api.Entity;
using City.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace City.Api.Implementation
{
    public class CityRepository : ICityRepository
    {
        private readonly DbCityContext _context;

        public CityRepository(DbCityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<IEnumerable<Citie>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(u=>u.Name).ToListAsync();
        }

        public async Task<(IEnumerable<Citie>, PaginationMetaData)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            //if (string.IsNullOrEmpty(name) && (string.IsNullOrWhiteSpace(searchQuery)))  //I comment this check since I am implementing pagination.
            //{
            //    return await GetCitiesAsync();
            //}

            var collection = _context.Cities as IQueryable<Citie>;

            if(!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = _context.Cities.Where(u => u.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = _context.Cities.Where(u=>u.Name.Contains(searchQuery) || (u.Description != null && u.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(u => u.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();  //Implementing pagination.

            return (collectionToReturn, paginationMetadata);

            //name = name.Trim();
            //return await _context.Cities.Where(u=>u.Name == name).OrderBy(u=>u.Name).ToListAsync();

        }

        public async Task<Citie?> GetCityAsync(int cityId, bool includePointOfInterest)
        {
            if (includePointOfInterest)
            {
                return await _context.Cities.Include(u => u.PointsOfInterest).Where(u => u.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities.Where(u => u.Id == cityId).FirstOrDefaultAsync();
        }


        public async Task<PointsOfInterest> GetPointOfInterestsForCityAsync(int cityId, int pointsOfInterestId)
        {
            return await _context.PointsOfInterests.Where(u => u.CityId == cityId && u.Id == pointsOfInterestId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointsOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterests.Where(u=>u.CityId == cityId).ToListAsync();
        }

        //public async Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
        //{
        //    return await _context.PointsOfInterests.AnyAsync(u=>u.Id == cityId && u.Name == cityName);
        //}

        public async Task<bool> CityExistAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(u => u.Id == cityId);
        }

        public async Task CreatePointOfInterestForCityAsync(int cityId, PointsOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);

            if (city != null)
            {
                city?.PointsOfInterest.Add(pointOfInterest);
            }
        }

        public void DeletePointOfInterestForCity(PointsOfInterest pointOfInterest)
        {
            _context.PointsOfInterests.Remove(pointOfInterest);
        }
        

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
        
    }
}
