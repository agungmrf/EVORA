using API.Contracts;
using API.DTOs.Provinces;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class ProvinceController : ControllerBase
{
    private readonly IProvinceRepository _provinceRepository;

    public ProvinceController(IProvinceRepository provinceRepository)
    {
        _provinceRepository = provinceRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _provinceRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (ProvinceDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<ProvinceDto>>(data));
    }
    
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _provinceRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<ProvinceDto>((ProvinceDto)result));
    }
    
    [HttpPost]
    public IActionResult Create(ProvinceDto provinceDto)
    {
        try
        {
            var result = _provinceRepository.Create(provinceDto);

            return Ok(new ResponseOKHandler<ProvinceDto>("Data has been created successfully")
                { Data = (ProvinceDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }
    
    [HttpPut]
    public IActionResult Update(ProvinceDto provinceDto)
    {
        try
        {
            var entity = _provinceRepository.GetByGuid(provinceDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            Province toUpdate = provinceDto;
            
            _provinceRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<ProvinceDto>("Data has been updated successfully")
                { Data = (ProvinceDto)toUpdate });
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
            var entity = _provinceRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _provinceRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}