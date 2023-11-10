using AutoMapper;
using City.Api.Entity;
using City.Api.Model;
using City.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace City.Api.Controllers
{

    [Route("api/cities/{cityId}/pointofinterest")]
    [Authorize("MostBeFromLagos")]
    [ApiController]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly ILocalMailService _mailService;
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepo;

        public PointOfInterestController(ILogger<PointOfInterestController> logger, ILocalMailService mailService, ICityRepository cityRepo, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));    
            _cityRepo = cityRepo ?? throw new ArgumentNullException(nameof(cityRepo));
            
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            //var cityName = User.Claims.FirstOrDefault(u => u.Type == "city") ?.Value;

            //if(!await _cityRepo.CityNameMatchesCityId(cityName, cityId))
            //{
            //    return Forbid();
            //}

            if(!await _cityRepo.CityExistAsync(cityId))
            {
                _logger.LogInformation($"City with Id {cityId} was not found when accessing point of interest");
                return NotFound();
            }

            var getpointOfInterestFromCity =  await _cityRepo.GetPointsOfInterestForCityAsync(cityId);

            if(getpointOfInterestFromCity == null)
            {
                return NotFound();
            }

            //return Ok(_mapper.Map<PointOfInterestDto>(getpointOfInterestFromCity));
            return Ok(getpointOfInterestFromCity);
         
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        [HttpGet("{pointofinterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if(! await _cityRepo.CityExistAsync(cityId))
            {
                NotFound();
            }

            var pointsOfInterest = await _cityRepo.GetPointOfInterestsForCityAsync(cityId, pointOfInterestId);
            if(pointsOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointsOfInterest));
           
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterestForCreationDto)
        {
           if(!await _cityRepo.CityExistAsync(cityId))
           {
                return NotFound();
           }

            //var maxPointOfInterestId = _citiesDtoStore.Cities.SelectMany(x => x.PointsOfInterests).Max(v => v.Id); //adding a +1 to the highest Id.

            var finalPointOfInterest = _mapper.Map<PointsOfInterest>(pointOfInterestForCreationDto);

            await _cityRepo.CreatePointOfInterestForCityAsync(cityId, finalPointOfInterest);

            await _cityRepo.SaveChangesAsync();

            var createPointOfInterestToReturn = _mapper.Map<Model.PointOfInterestDto>(finalPointOfInterest);

            //var finalPointOfInterest = new PointOfInterestDto()
            //{
            //    Id = ++maxPointOfInterestId,
            //    Name = pointOfInterestForCreationDto.Name,
            //    Description = pointOfInterestForCreationDto.Description,
            //};

            //city.PointsOfInterests.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityid = cityId,
                    pointOfInterestId = createPointOfInterestToReturn.Id
                },
                createPointOfInterestToReturn
                );
            
        }


        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult<PointOfInterestDto>> UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterestForUpdateDto)
        {
            if(!await _cityRepo.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityRepo.GetPointOfInterestsForCityAsync(cityId, pointOfInterestId);

            if(pointOfInterestEntity == null)
            {
                NotFound();
            }
            
            _mapper.Map(pointOfInterestForUpdateDto, pointOfInterestEntity);

            await _cityRepo.SaveChangesAsync();

            return NoContent();
              
        }

        [HttpPatch("{pointofinterestid}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, 
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            if(!await _cityRepo.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityRepo.GetPointOfInterestsForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);
            

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest();
            }

            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);
            await _cityRepo.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]
        public async Task<ActionResult> Delete(int cityId, int pointOfInterestId)
        {
            if (!await _cityRepo.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _cityRepo.GetPointOfInterestsForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityRepo.DeletePointOfInterestForCity(pointOfInterestEntity);
            await _cityRepo.SaveChangesAsync();

            _mailService.Send("Point of interest deleted", $"Point of interest {pointOfInterestEntity.Name} with Id {pointOfInterestEntity.Id} was deleted");

            return NoContent();
        }




    }
}
