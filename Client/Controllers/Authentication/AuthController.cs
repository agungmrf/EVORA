using API.Contracts;
using API.DTOs.Accounts;
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers.Authentication
{
    public class AuthController : Controller
    {
        private readonly IAccountRepos _accountRepository;

        public AuthController(IAccountRepos accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await _accountRepository.Login(login);

            if (result.Status == "OK")
            {
                Console.WriteLine("Berhasil Login");
                Console.WriteLine(result.Data.Token);
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                
                return RedirectToAction("Index", "User");

            }
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
    }
}
