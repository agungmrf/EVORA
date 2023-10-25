using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Utilities.Handler;
using API.Utilities.Handlers;
using API.Utilities.Validations.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
[EnableCors]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailHandler _emailHandler;
    private readonly IEmployeeRepository _employeeRepository;


    public AccountController(IAccountRepository accountRepository, IEmailHandler emailHandler, IEmployeeRepository employeeRepository)
    {
        _accountRepository = accountRepository;
        _emailHandler = emailHandler;
        _employeeRepository = employeeRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _accountRepository.GetAll();
        if (!result.Any())
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));

        var data = result.Select(x => (AccountDto)x);

        return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
    }

    //[Authorize(Roles = "admin, user, manager")]
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _accountRepository.GetByGuid(guid);
        if (result is null)
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));

        return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
    }

    [HttpPost]
    public IActionResult Create(CreateAccountDto createAccountDto)
    {
        try
        {
            Account toCreate = createAccountDto;
            toCreate.Password = HashingHandler.HashPassword(createAccountDto.Password);

            var result = _accountRepository.Create(toCreate);

            return Ok(new ResponseOKHandler<AccountDto>("Data has been created successfully")
                { Data = (AccountDto)result });
        }
        catch (ExceptionHandler ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }

    [HttpPut]
    public IActionResult Update(AccountDto accountDto)
    {
        try
        {
            var entity = _accountRepository.GetByGuid(accountDto.Guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            Account toUpdate = accountDto;
            toUpdate.CreatedDate = entity.CreatedDate;
            toUpdate.Password =
                HashingHandler.HashPassword(accountDto.Password); 

            _accountRepository.Update(toUpdate);

            return Ok(new ResponseOKHandler<AccountDto>("Data has been updated successfully")
                { Data = (AccountDto)toUpdate });
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
            var entity = _accountRepository.GetByGuid(guid);
            if (entity is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _accountRepository.Delete(entity);

            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public IActionResult ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        try
        {
            var employee = _employeeRepository.GetByEmail(forgotPasswordDto.Email);

            if (employee is null)
                return NotFound(new ResponseNotFoundHandler("Email is invalid!"));
            var account = _accountRepository.GetByGuid(employee.Guid);

            if (account is null)
                return NotFound(new ResponseNotFoundHandler("Email is invalid!"));

            var otp = new Random().Next(111111, 999999);
            account.ExpiredDate = DateTime.Now.AddMinutes(5);
            account.IsUsed = false;
            account.Otp = otp;

            _accountRepository.Update(account);

            _emailHandler.Send("Forgot Password", $"Your OTP is {otp}",
                forgotPasswordDto.Email);

            return Ok(new ResponseOKHandler<object>("OTP has been sent to your email"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create OTP", ex.Message));
        }
    }

    [HttpPut("change-password")]
    [AllowAnonymous]
    public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
    {
        try
        {
            var employees = _employeeRepository.GetAll();
            var accounts = _accountRepository.GetAll();

            if (!employees.Any() || !accounts.Any()) return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            
            var getEmployee = employees.FirstOrDefault(emp => emp.Email == changePasswordDto.Email);

            var getAccount = accounts.FirstOrDefault(acc => acc.Guid == getEmployee?.Guid);

            if (getEmployee == null || getAccount == null)
                return NotFound(new ResponseNotFoundHandler("Employee or account data not found"));

            if (getAccount.Otp != changePasswordDto.Otp)
                return BadRequest(new ResponseValidatorHandler("OTP is invalid"));

            if (getAccount.IsUsed) return BadRequest(new ResponseValidatorHandler("OTP has not been used yet"));

            if (getAccount.ExpiredDate < DateTime.Now)
                return BadRequest(new ResponseValidatorHandler("OTP has expired"));

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                return BadRequest(new ResponseValidatorHandler("NewPassword and ConfirmPassword do not match"));

            getAccount.Password = HashingHandler.HashPassword(changePasswordDto.NewPassword);
            getAccount.IsUsed = true;
            getAccount.ModifiedDate = DateTime.Now;

            var updateResult = _accountRepository.Update(getAccount);

            if (!updateResult)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseServerErrorHandler("Failed to update data"));

            return Ok(new ResponseOKHandler<string>("Password has been changed successfully"));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to process the request", ex.Message));
        }
    }
}