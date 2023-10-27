using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handler;
using API.Utilities.Handlers;
using API.Utilities.Validations.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

<<<<<<< Updated upstream
=======
[ApiController]
[Route("api/[controller]")]
[EnableCors]
>>>>>>> Stashed changes
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailHandler _emailHandler;
    private readonly IEmployeeRepository _employeeRepository;
<<<<<<< Updated upstream


    public AccountController(IAccountRepository accountRepository, IEmailHandler emailHandler, IEmployeeRepository employeeRepository)
=======
    private readonly ICustomerRepository _customerRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IGenerateTokenHandler _tokenService;
    private readonly EvoraDbContext _dbContext;


    public AccountController(IAccountRepository accountRepository, IEmailHandler emailHandler, IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IAccountRoleRepository accountRoleRepository, IGenerateTokenHandler tokenService, EvoraDbContext dbContext, ICustomerRepository customerRepository)
>>>>>>> Stashed changes
    {
        _accountRepository = accountRepository;
        _emailHandler = emailHandler;
        _employeeRepository = employeeRepository;
<<<<<<< Updated upstream
=======
        _roleRepository = roleRepository;
        _accountRoleRepository = accountRoleRepository;
        _tokenService = tokenService;
        _dbContext = dbContext;
        _customerRepository = customerRepository;
>>>>>>> Stashed changes
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        // Mengambil semua data dari database.
        var result = _accountRepository.GetAll();
        if (!result.Any())
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));

        // Mengubah IEnumerable<Account> menjadi IEnumerable<AccountDto>
        var data = result.Select(x => (AccountDto)x);

        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
    }

    // Untuk menangani request GET dengan route /api/[controller]/guid.
    //[Authorize(Roles = "admin, user, manager")]
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Mengambil data dari database berdasarkan guid.
        var result = _accountRepository.GetByGuid(guid);
        if (result is null)
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));

        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
    }

    // Untuk menangani request POST dengan route /api/[controller].
    [HttpPost]
    public IActionResult Create(CreateAccountDto createAccountDto)
    {
        try
        {
            Account toCreate = createAccountDto;
            // Untuk menghash password sebelum disimpan ke database
            toCreate.Password = HashingHandler.HashPassword(createAccountDto.Password);

            var result = _accountRepository.Create(toCreate);

            // Setelah data berhasil dibuat, maka akan mengembalikan response 201 Created.
            return Ok(new ResponseOKHandler<AccountDto>("Data has been created successfully")
            { Data = (AccountDto)result });
        }
<<<<<<< Updated upstream
        catch (ExceptionHandler ex) // ExceptionHandler untuk menangani exception ketika terjadi error
=======
        catch (ExceptionHandler ex)
>>>>>>> Stashed changes
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }

    // Untuk menangani request PUT dengan route /api/[controller].
    [HttpPut]
    public IActionResult Update(AccountDto accountDto)
    {
        try
        {
            // Mengambil data di database berdasarkan guid.
            var entity = _accountRepository.GetByGuid(accountDto.Guid);
            if (entity is null)
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            Account toUpdate = accountDto;
            toUpdate.CreatedDate = entity.CreatedDate; // Menyalin CreatedDate dari entity yang diambil dari database.
            toUpdate.Password =
<<<<<<< Updated upstream
                HashingHandler.HashPassword(accountDto
                    .Password); // Mengambil password dari request body, kemudian dihash sebelum disimpan ke database
=======
                HashingHandler.HashPassword(accountDto.Password);
>>>>>>> Stashed changes

            _accountRepository.Update(toUpdate);

            // Setelah data berhasil diubah, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<AccountDto>("Data has been updated successfully")
            { Data = (AccountDto)toUpdate });
        }
<<<<<<< Updated upstream
        catch (ExceptionHandler ex) // ExceptionHandler untuk menangani exception ketika terjadi error
=======
        catch (ExceptionHandler ex)
>>>>>>> Stashed changes
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }

    // Untuk menangani request DELETE dengan route /api/[controller].
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Menghapus data di database berdasarkan guid.
            var entity = _accountRepository.GetByGuid(guid);
            if (entity is null)
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            _accountRepository.Delete(entity);

            // Setelah data berhasil dihapus, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
<<<<<<< Updated upstream
        catch (ExceptionHandler ex) // ExceptionHandler untuk menangani exception ketika terjadi error
=======
        catch (ExceptionHandler ex)
>>>>>>> Stashed changes
        {
            // ResponseServerErrorHandler untuk response 500 Internal Server Error
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
            // Cari data employee berdasarkan email
            var employee = _employeeRepository.GetByEmail(forgotPasswordDto.Email);

            if (employee is null)
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Email is invalid!"));
            // Cari data akun berdasarkan guid employee
            var account = _accountRepository.GetByGuid(employee.Guid);

            if (account is null)
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Email is invalid!"));

            var otp = new Random().Next(111111, 999999); // Generate OTP secara random
            account.ExpiredDate = DateTime.Now.AddMinutes(5); // Set waktu kedaluwarsa OTP menjadi 5 menit dari sekarang
            account.IsUsed = false; // Set OTP sebagai belum digunakan
            account.Otp = otp; // Set OTP yang telah dibuat

            _accountRepository.Update(account);

            // Kirim email ke email yang diberikan
            _emailHandler.Send("Forgot Password", $"Your OTP is {otp}",
                forgotPasswordDto.Email); // Kirim email ke email yang diberikan

            // Mengembalikan respons sukses dengan OTP yang telah dibuat
            return Ok(new ResponseOKHandler<object>("OTP has been sent to your email"));
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error
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
            // Cari data employee dan account
            var employees = _employeeRepository.GetAll();
            var accounts = _accountRepository.GetAll();

            if (!employees.Any() || !accounts.Any()) return NotFound(new ResponseNotFoundHandler("Data Not Found"));
<<<<<<< Updated upstream
            // Cari data employee dan account berdasarkan email
=======

>>>>>>> Stashed changes
            var getEmployee = employees.FirstOrDefault(emp => emp.Email == changePasswordDto.Email);
            // Cari data account berdasarkan guid employee
            var getAccount = accounts.FirstOrDefault(acc => acc.Guid == getEmployee?.Guid);

            if (getEmployee == null || getAccount == null)
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Employee or account data not found"));

            // Periksa apakah OTP cocok dengan yang dikirim dalam permintaan
            if (getAccount.Otp != changePasswordDto.Otp)
                return BadRequest(new ResponseValidatorHandler("OTP is invalid"));

            // Periksa apakah OTP sudah digunakan sebelumnya
            if (getAccount.IsUsed) return BadRequest(new ResponseValidatorHandler("OTP has not been used yet"));

            // Periksa apakah OTP sudah kadaluwarsa
            if (getAccount.ExpiredDate < DateTime.Now)
                return BadRequest(new ResponseValidatorHandler("OTP has expired"));

            // Periksa apakah NewPassword dan ConfirmPassword cocok
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                return BadRequest(new ResponseValidatorHandler("NewPassword and ConfirmPassword do not match"));

            // Perbarui kata sandi dengan yang baru dan tandai OTP sebagai digunakan
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
<<<<<<< Updated upstream
}
=======

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
}
>>>>>>> Stashed changes
