using API.Contracts;
using API.DTOs.Cities;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityRepository _cityRepository;

    public CityController(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _cityRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (CityDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<CityDto>>(data));
    }
    
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _cityRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<CityDto>((CityDto)result));
    }
    
    [HttpPost]
    public IActionResult Create(CityDto cityDto)
    {
        try
        {
            var result = _cityRepository.Create(cityDto);

            return Ok(new ResponseOKHandler<CityDto>("Data has been created successfully")
                { Data = (CityDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }
    
    [HttpPut]
    public IActionResult Update(CityDto cityDto)
    {
        try
        {
            var entity = _cityRepository.GetByGuid(cityDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            City toUpdate = cityDto;
            
            _cityRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<CityDto>("Data has been updated successfully")
                { Data = (CityDto)toUpdate });
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
            var entity = _cityRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _cityRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}