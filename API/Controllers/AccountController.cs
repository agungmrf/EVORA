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
        catch (ExceptionHandler ex) // ExceptionHandler untuk menangani exception ketika terjadi error
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
                HashingHandler.HashPassword(accountDto
                    .Password); // Mengambil password dari request body, kemudian dihash sebelum disimpan ke database

            _accountRepository.Update(toUpdate);

            // Setelah data berhasil diubah, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<AccountDto>("Data has been updated successfully")
                { Data = (AccountDto)toUpdate });
        }
        catch (ExceptionHandler ex) // ExceptionHandler untuk menangani exception ketika terjadi error
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
        catch (ExceptionHandler ex) // ExceptionHandler untuk menangani exception ketika terjadi error
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
            // Cari data employee dan account berdasarkan email
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
}