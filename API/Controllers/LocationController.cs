using API.Contracts;
using API.DTOs.Locations;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;

    public LocationController(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _locationRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (LocationDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<LocationDto>>(data));
    }
    [HttpGet("detail")]
    public IActionResult GetAllDetail()
    {
        var result = _locationRepository.GetAllDetailLocation();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (DetailLocationDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<DetailLocationDto>>(data));
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _locationRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<LocationDto>((LocationDto)result));
    }
    [HttpGet("detail/{guid}")]
    public IActionResult GetDetailByGuid(Guid guid)
    {
        var result = _locationRepository.GetDetailLocation(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<DetailLocationDto>(result));
    }
    [HttpPost]
    public IActionResult Create(LocationDto locationDto)
    {
        try
        {
            var result = _locationRepository.Create(locationDto);

            return Ok(new ResponseOKHandler<LocationDto>("Data has been created successfully")
                { Data = (LocationDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }

    [HttpPut]
    public IActionResult Update(LocationDto locationDto)
    {
        try
        {
            var entity = _locationRepository.GetByGuid(locationDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            Location toUpdate = locationDto;
            
            _locationRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<LocationDto>("Data has been updated successfully")
                { Data = (LocationDto)toUpdate });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            var entity = _locationRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _locationRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}