using AutoMapper;
using City.Api.Model;
using City.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace City.Api.Controllers
{
    [Route("api/cities")]
    [Authorize]
    [ApiController]
    public class CitiesController : ControllerBase
    {

        //private readonly CitiesDtoStore _citiesDtoStore;

        //public CitiesController(CitiesDtoStore citiesDtoStore)
        //{
        //    _citiesDtoStore = citiesDtoStore;
        //}

        private readonly ICityRepository _cityRepo;
        private readonly IMapper _mapper;
        const int maxPageSize = 20;

        public CitiesController(ICityRepository cityRepo, IMapper mapper)
        {
            _cityRepo = cityRepo ?? throw new ArgumentNullException(nameof(cityRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDtoWithOutPointOfInterest>>>GetCities(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if(pageNumber > maxPageSize)
            {
                pageNumber = maxPageSize;
            }
            var (cityEntities, paginationMetadata) = await _cityRepo.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);
            
            Response.Headers.Add("X-Pegination", JsonSerializer.Serialize(paginationMetadata));
            
            return Ok(_mapper.Map<IEnumerable<CityDtoWithOutPointOfInterest>>(cityEntities));  //implementing Automapper.

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCities(int id, bool includePointOfInterest = false)
        {
            //find city by id.
            var city = await _cityRepo.GetCityAsync(id, includePointOfInterest);

            if (city == null)
            {
                return NotFound();
            }


            if (includePointOfInterest)
            {
                return Ok(_mapper.Map<CitiesDto>(city));
            }

            return Ok(_mapper.Map<CityDtoWithOutPointOfInterest>(city));
            
        }

    }
}
