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

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await _accountRepository.Login(login);

            if (result.Status == "OK")
            {

                HttpContext.Session.SetString("JWToken", result.Data.Token);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
    }
}
