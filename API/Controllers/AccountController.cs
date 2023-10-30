using System.Net;
using System.Security.Claims;
using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.DTOs.Employees;
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
    private readonly ICustomerRepository _customerRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IGenerateTokenHandler _tokenService;
    private readonly EvoraDbContext _dbContext;


    public AccountController(IAccountRepository accountRepository, IEmailHandler emailHandler, IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IAccountRoleRepository accountRoleRepository, IGenerateTokenHandler tokenService, EvoraDbContext dbContext, ICustomerRepository customerRepository)
    {
        _accountRepository = accountRepository;
        _emailHandler = emailHandler;
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;
        _accountRoleRepository = accountRoleRepository;
        _tokenService = tokenService;
        _dbContext = dbContext;
        _customerRepository = customerRepository;
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
    
    [HttpPost("register-employee")]
    [AllowAnonymous]
    public IActionResult RegisterEmp(RegisterEmpDto registerEmpDto)
    {
        using var transaction = _dbContext.Database.BeginTransaction();

        try
        {
            Account accountToCreate = new AccountDto
            {
                IsUsed = true,
                ExpiredDate = DateTime.Now.AddMinutes(5),
                Otp = 111111,
                Password = HashingHandler.HashPassword(registerEmpDto.Password)
            };
            
            _accountRepository.Create(accountToCreate);
            
            Employee employeeToCreate = new EmployeeDto
            {
                FirstName = registerEmpDto.FirstName,
                LastName = registerEmpDto.LastName,
                BirthDate = registerEmpDto.BirthDate,
                Gender = registerEmpDto.Gender,
                HiringDate = registerEmpDto.HiringDate,
                Email = registerEmpDto.Email,
                PhoneNumber = registerEmpDto.PhoneNumber,
                AccountGuid = accountToCreate.Guid,
            };
            // Generate NIK baru dengan memanggil method Nik dari class GenerateHandler.
            employeeToCreate.Nik = GenerateHandler.Nik(_employeeRepository.GetLastNik());
            _employeeRepository.Create(employeeToCreate);
            
           
            var accountRole = _accountRoleRepository.Create(new AccountRole
            {
                AccountGuid = accountToCreate.Guid,
                RoleGuid = _roleRepository.getDefaultRoleEmp() ?? throw new Exception("Default role not found")
            });
            
            // Commit transaksi jika semuanya berhasil
            transaction.Commit();
        }
        catch (ExceptionHandler ex)
        {
            // Rollback transaksi jika terjadi kesalahan
            transaction.Rollback();
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }

        // Jika berhasil, maka akan mengembalikan response 200 OK
        return Ok(new ResponseOKHandler<RegisterEmpDto>("Data has been created successfully") { Data = registerEmpDto });
    }
    
    [HttpPost("register-customer")]
    [AllowAnonymous]
    public IActionResult RegisterCust(RegisterCustDto registerCustDto)
    {
        using var transaction = _dbContext.Database.BeginTransaction();

        try
        {
            Account accountToCreate = new AccountDto
            {
                IsUsed = true,
                ExpiredDate = DateTime.Now.AddMinutes(5),
                Otp = 111111,
                Password = HashingHandler.HashPassword(registerCustDto.Password)
            };
            
            _accountRepository.Create(accountToCreate);
            
            Customer customerToCreate = new CustomerDto
            {
                FirstName = registerCustDto.FirstName,
                LastName = registerCustDto.LastName,
                BirthDate = registerCustDto.BirthDate,
                Gender = registerCustDto.Gender,
                Email = registerCustDto.Email,
                PhoneNumber = registerCustDto.PhoneNumber,
                AccountGuid = accountToCreate.Guid,
            };
            _customerRepository.Create(customerToCreate);
            
           
            var accountRole = _accountRoleRepository.Create(new AccountRole
            {
                AccountGuid = accountToCreate.Guid,
                RoleGuid = _roleRepository.getDefaultRoleCust() ?? throw new Exception("Default role not found")
            });
            
            // Commit transaksi jika semuanya berhasil
            transaction.Commit();
        }
        catch (ExceptionHandler ex)
        {
            // Rollback transaksi jika terjadi kesalahan
            transaction.Rollback();
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }

        // Jika berhasil, maka akan mengembalikan response 200 OK
        return Ok(new ResponseOKHandler<RegisterCustDto>("Data has been created successfully") { Data = registerCustDto });
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginDto loginDto)
    {
        try
        {
            var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);

            if (getEmployee is null)
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            var getAccount = _accountRepository.GetByGuid((Guid)getEmployee.AccountGuid);

            if (!HashingHandler.VerifyPassword(loginDto.Password, getAccount.Password))
                return BadRequest(new ResponseValidatorHandler("Password is invalid"));

            // Generate token
            var claims = new List<Claim>();
            claims.Add(new Claim("Email", getEmployee.Email));
            claims.Add(new Claim("FullName", string.Concat(getEmployee.FirstName, " ", getEmployee.LastName)));

            // Untuk mendapatkan role dari akun yang sedang login
            var getRoleNames = from ar in _accountRoleRepository.GetAll()
                join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                where ar.AccountGuid == getEmployee.Guid
                select r.Name;

            // Jika akun memiliki lebih dari satu role, maka akan ditambahkan ke claims
            foreach (var roleName in getRoleNames) claims.Add(new Claim(ClaimTypes.Role, roleName));

            var generateToken = _tokenService.Generate(claims);

            return Ok(new ResponseOKHandler<object>("Login success", new { Token = generateToken }));
        }
        catch (ExceptionHandler ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to process the request", ex.Message));
        }
    }
    
    [Authorize]
    [HttpGet("GetClaims/{token}")]
    public IActionResult GetClaims(string token)
    {
        var claims = _tokenService.ExtractClaimsFromJwt(token);
        return Ok(new ResponseOKHandler<ClaimsDto> {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Claims has been retrieved",
            Data = claims
        });
    }
}