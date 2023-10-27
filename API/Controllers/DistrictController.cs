using API.Contracts;
using API.DTOs.Districts;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class DistrictController  : ControllerBase
{
    private readonly IDistrictRepository _districtRepository;

    public DistrictController(IDistrictRepository districtRepository)
    {
        _districtRepository = districtRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _districtRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (DistrictDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<DistrictDto>>(data));
    }
    
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _districtRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<DistrictDto>((DistrictDto)result));
    }
    
    [HttpPost]
    public IActionResult Create(DistrictDto districtDto)
    {
        try
        {
            var result = _districtRepository.Create(districtDto);

            return Ok(new ResponseOKHandler<DistrictDto>("Data has been created successfully")
                { Data = (DistrictDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }
    
    [HttpPut]
    public IActionResult Update(DistrictDto districtDto)
    {
        try
        {
            var entity = _districtRepository.GetByGuid(districtDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            District toUpdate = districtDto;
            
            _districtRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<DistrictDto>("Data has been updated successfully")
                { Data = (DistrictDto)toUpdate });
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
            var entity = _districtRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _districtRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}