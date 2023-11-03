using API.Contracts;
using API.DTOs.PackageEvents;
using API.DTOs.TransactionEvents;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class PackageEventController : ControllerBase
{
    private readonly IPackageEventRepository _packageEventRepository;

    public PackageEventController(IPackageEventRepository packageEventRepository)
    {
        _packageEventRepository = packageEventRepository;
    }
    
        [HttpGet]
    public IActionResult GetAll()
    {
        var result = _packageEventRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (PackageEventDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<PackageEventDto>>(data));
    }
    [HttpGet("best-deal")]
    public IActionResult GetByBestDeal()
    {
        var packages = _packageEventRepository.GetAll();
        var firstFourData = packages.Take(4);
        if (!firstFourData.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = firstFourData.Select(x => (PackageEventDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<PackageEventDto>>(data));
    }
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _packageEventRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<PackageEventDto>((PackageEventDto)result));
    }
    
    [HttpPost]
    public IActionResult Create(PackageEventDto packageEventDtoDto)
    {
        try
        {
            var result = _packageEventRepository.Create(packageEventDtoDto);

            return Ok(new ResponseOKHandler<PackageEventDto>("Data has been created successfully")
                { Data = (PackageEventDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }
    
    [HttpPut]
    public IActionResult Update(PackageEventDto packageEventDto)
    {
        try
        {
            var entity = _packageEventRepository.GetByGuid(packageEventDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            PackageEvent toUpdate = packageEventDto;
            
            _packageEventRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<PackageEventDto>("Data has been updated successfully")
                { Data = (PackageEventDto)toUpdate });
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
            var entity = _packageEventRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _packageEventRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}