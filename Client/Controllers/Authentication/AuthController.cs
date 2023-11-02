using API.DTOs.Accounts;
using API.Utilities.Validations.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.Authentication
{
    public class AuthController : Controller
    {
        private readonly IAccountRepos _accountRepository;
        private readonly IGetCustomerRepository getcustomerRepository;

        public AuthController(IAccountRepos accountRepository, IGetCustomerRepository getcustomerRepository)
        {
            _accountRepository = accountRepository;
            this.getcustomerRepository = getcustomerRepository;
        }

        public async Task<IActionResult> Login()
        {
            ViewBag.MessageErr = "";
            string jwtToken = HttpContext.Session.GetString("JWToken");
            if (jwtToken != null)
            {
                var dataUser = await _accountRepository.GetClaims(jwtToken);
                var role = dataUser.Data.Role;
                if (role[0] == "user")
                {
                    return RedirectToAction("Index", "User");
                }
                else if (role[0] == "Staff")
                {
                    return RedirectToAction("Index", "Staff");
                }
                else if (role[0] == "Admin")
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            ViewBag.MessageErr = "";
            var email = login.Email;
            var result = await _accountRepository.Login(login);

            if (result.Status == "OK")
            {
                Console.WriteLine("Berhasil Login");
                var token = result.Data.Token;
                HttpContext.Session.SetString("JWToken", token);
                var dataUser = await _accountRepository.GetClaims(token);
                var role = dataUser.Data.Role;

                if (role[0] == "user")
                {
                    return RedirectToAction("Index", "User");
                }
                else if (role[0] == "Staff")
                {
                    return RedirectToAction("Index", "Staff");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                ViewBag.MessageErr = "Password atau Email Salah";
                Console.WriteLine("Gagal Login");
                return View();
            }

            return View();
        }

        [HttpGet("Logout/")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(RegisterCustDto registerCustDto)
        {
            var result = await _accountRepository.RegisterUser(registerCustDto);

            if (result.Status == "OK")
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        public IActionResult ForgotPassword()
        {
            ViewBag.Email = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            ViewBag.Email = "";
            var result = await _accountRepository.ForgotPassword(forgotPasswordDto);

            if (result.Status == "OK")
            {
                ViewBag.Email = forgotPasswordDto.Email;
                return RedirectToAction("ResetPassword");
            }
            else
            {
                
                ViewBag.MessageErr = "Email Tidak Ditemukan";
                return View();
            }
        }


        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ChangePasswordDto changePasswordDto)
        {
            var result = await _accountRepository.ChangePassword(changePasswordDto);

            if (result.Status == "OK")
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
    }
}
