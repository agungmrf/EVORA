using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _customerRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (CustomerDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<CustomerDto>>(data));
    }
    
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _customerRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<CustomerDto>((CustomerDto)result));
    }
    [HttpGet("email/{email}")]
    public IActionResult GetByEmail(string email)
    {
        var result = _customerRepository.GetByEmail(email);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<CustomerDto>((CustomerDto)result));
    }
    [HttpPost]
    public IActionResult Create(CustomerDto customerDto)
    {
        try
        {
            Customer toCreate = customerDto;

            var result = _customerRepository.Create(toCreate);

            return Ok(new ResponseOKHandler<CustomerDto>("Data has been created successfully")
                { Data = (CustomerDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }
    
    [HttpPut]
    public IActionResult Update(CustomerDto customerDto)
    {
        try
        {
            var entity = _customerRepository.GetByGuid(customerDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            Customer toUpdate = customerDto;
            toUpdate.AccountGuid = entity.AccountGuid;

            _customerRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<string>("Data has been updated successfully"));
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
            var entity = _customerRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _customerRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}