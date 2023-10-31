using API.Contracts;
using API.DTOs.Accounts;
using Client.Contracts;
using Client.Models;
using Client.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
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
                else if(role[0] == "Staff")
                {
                    return RedirectToAction("Index", "Staff");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpGet("Logout/")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
