using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

<<<<<<< Updated upstream
=======
[ApiController]
[Route("api/[controller]")]
>>>>>>> Stashed changes
public class AccountRoleController : ControllerBase
{
    private readonly IAccountRoleRepository _accountRoleRepository;

    public AccountRoleController(IAccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }
<<<<<<< Updated upstream
=======

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _accountRoleRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (AccountRoleDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<AccountRoleDto>>(data));
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _accountRoleRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<AccountRoleDto>((AccountRoleDto)result));
    }

    [HttpPost]
    public IActionResult Create(AccountRoleDto accountRoleDto)
    {
        try
        {
            var result = _accountRoleRepository.Create(accountRoleDto);

            return Ok(new ResponseOKHandler<AccountRoleDto>("Data has been created successfully")
            { Data = (AccountRoleDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }

    [HttpPut]
    public IActionResult Update(AccountRoleDto accountRoleDto)
    {
        try
        {
            var entity = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            AccountRole toUpdate = accountRoleDto;

            _accountRoleRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<AccountRoleDto>("Data has been updated successfully")
            { Data = (AccountRoleDto)toUpdate });
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
            var entity = _accountRoleRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _accountRoleRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
>>>>>>> Stashed changes
}