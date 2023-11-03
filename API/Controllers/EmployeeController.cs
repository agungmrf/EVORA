using API.Contracts;
using API.Data;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly EvoraDbContext _dbContext;
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IRoleRepository _roleRepository;

    public EmployeeController(IEmployeeRepository employeeRepository, EvoraDbContext dbContext, IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository)
    {
        _employeeRepository = employeeRepository;
        _dbContext = dbContext;
        _accountRepository = accountRepository;
        _accountRoleRepository = accountRoleRepository;
        _roleRepository = roleRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _employeeRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        var data = result.Select(x => (EmployeeDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<EmployeeDto>>(data));
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _employeeRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)result));
    }
    [HttpGet("email/{email}")]
    public IActionResult GetByEmail(string email)
    {
        var result = _employeeRepository.GetByEmail(email);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)result));
    }
    [HttpPost]
    public IActionResult Create(EmployeeDto employeeDto)
    {
        try
        {
            Employee toCreate = employeeDto;
            toCreate.Nik = GenerateHandler.Nik(_employeeRepository.GetLastNik());

            var result = _employeeRepository.Create(toCreate);

            return Ok(new ResponseOKHandler<EmployeeDto>("Data has been created successfully")
                { Data = (EmployeeDto)result });
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }

    [HttpPut]
    public IActionResult Update(UpdateEmployeeDto employeeDto)
    {
        using var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            var entity = _employeeRepository.GetByGuid(employeeDto.Guid);
            var RoleGuid = _roleRepository.getDefaultRoleEmp(employeeDto.Role);

            var accountRole = _accountRoleRepository.GetRoleGuidsByAccountGuid(entity.AccountGuid);
            if (entity is null || !accountRole.Any())
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            var firstAccountRole = accountRole.FirstOrDefault();

            Employee toUpdate = (EmployeeDto)employeeDto;
            toUpdate.Nik = entity.Nik;
            toUpdate.AccountGuid = entity.AccountGuid;

            AccountRole toUpdateRole = firstAccountRole;
            toUpdateRole.RoleGuid = (Guid)RoleGuid;

            _employeeRepository.Update(toUpdate);

            _accountRoleRepository.Update(toUpdateRole);

            transaction.Commit();

            return Ok(new ResponseOKHandler<string>("Data has been updated successfully"));
        }
        catch (ExceptionHandler ex)
        {
            transaction.Rollback();
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            var entity = _employeeRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _employeeRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}