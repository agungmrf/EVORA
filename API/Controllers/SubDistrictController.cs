using API.Contracts;
using API.DTOs.SubDistricts;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class SubDistrictController : ControllerBase
{
    private readonly ISubDistrictRepository _subDistrictRepository;

    public SubDistrictController(ISubDistrictRepository subDistrictRepository)
    {
        _subDistrictRepository = subDistrictRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _subDistrictRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (SubDistrictDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<SubDistrictDto>>(data));
    }
    
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _subDistrictRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<SubDistrictDto>((SubDistrictDto)result));
    }
    
    [HttpPost]
    public IActionResult Create(SubDistrictDto subDistrictDto)
    {
        try
        {
            var result = _subDistrictRepository.Create(subDistrictDto);

            return Ok(new ResponseOKHandler<SubDistrictDto>("Data has been created successfully")
                { Data = (SubDistrictDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }
    
    [HttpPut]
    public IActionResult Update(SubDistrictDto subDistrictDto)
    {
        try
        {
            var entity = _subDistrictRepository.GetByGuid(subDistrictDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            SubDistrict toUpdate = subDistrictDto;
            
            _subDistrictRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<SubDistrictDto>("Data has been updated successfully")
                { Data = (SubDistrictDto)toUpdate });
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
            var entity = _subDistrictRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _subDistrictRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}