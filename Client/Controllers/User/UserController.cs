using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.PackageEvents;
using API.DTOs.TransactionEvents;
using API.DTOs.Locations;
using API.Models;
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Core.Types;
using NuGet.Common;
using System.Data;
using Newtonsoft.Json.Linq;

namespace Client.Controllers.User
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private readonly IUserRepository repository;
        private readonly IAddOrderRepos addOrderRepo;
        private readonly ILocationRepos locationRepository;
        private readonly IAccountRepos accountRepository;
        private readonly IGetCustomerRepository getcustomerRepository;
        public UserController(IUserRepository repository, IAddOrderRepos addOrder,
            ILocationRepos locationRepository, IAccountRepos accountRepository, IGetCustomerRepository getcustomerRepository)
        {
            this.repository = repository;
            this.addOrderRepo = addOrder;
            this.locationRepository = locationRepository;
            this.accountRepository = accountRepository;
            this.getcustomerRepository = getcustomerRepository;
        }

        public async Task<IActionResult> Index()
        {
            string jwtToken = HttpContext.Session.GetString("JWToken");
            var dataUser = await accountRepository.GetClaims(jwtToken);
            if (dataUser != null)
            {
                HttpContext.Session.SetString("Name", dataUser.Data.Name);
                ViewBag.Name = dataUser.Data.Name;
                ViewBag.Email = dataUser.Data.Email;
            }
            return View();
        }
        [HttpGet("User/Order")]
        public async Task<IActionResult> Order()
        {
            var result = await repository.Get();
            var cek = result.Data.ToList();
            Console.WriteLine("hasil : ", cek);
            return View(cek);
        }
        public async Task<IActionResult> AddOrder(Guid id)
        {

            Console.WriteLine(id);

            PackageEventDto dataPacket = new PackageEventDto();
            var result = await repository.Get(id);
            if (result != null)
            {
                dataPacket = result.Data;
            }
            ViewBag.Guid = dataPacket.Guid;
            ViewBag.Name = dataPacket.Name;
            ViewBag.Capacity = dataPacket.Capacity;
            ViewBag.Description = dataPacket.Description;
            ViewBag.Price = dataPacket.Price;

            return View();
        }

        public async Task<IActionResult> AddTransaction(InsertOrderTransactionDto transactionDto)
        {
            Console.WriteLine("Cek Order Packet");
            string jwtToken = HttpContext.Session.GetString("JWToken");
            var dataUser = await accountRepository.GetClaims(jwtToken);
            if (dataUser != null)
            {
                var email = dataUser.Data.Email;
                var getCust = getcustomerRepository.GetbyEmail(email);
                transactionDto.GuidCustomer = getCust.Result.Data.Guid;
            }
            
            var result = await addOrderRepo.Post(transactionDto);

            if (result != null)
            {
                Console.WriteLine(transactionDto.PackageGuid);
                Console.WriteLine(transactionDto.EventDate);
                Console.WriteLine(transactionDto.Street);
                Console.WriteLine(transactionDto.Disctrict);
                Console.WriteLine(transactionDto.SubDistrict);
                Console.WriteLine(transactionDto.ProvinceGuid);
                Console.WriteLine(transactionDto.CityGuid);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Histories()
        {
            return View();
        }
    }
}
