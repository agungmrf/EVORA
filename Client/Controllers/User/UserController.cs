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

namespace Client.Controllers.User
{
    public class UserController : Controller
    {
        private readonly IUserRepository repository;
        private readonly IAddOrderRepos addOrderRepo;
        private readonly ILocationRepos locationRepository;

        public UserController(IUserRepository repository, IAddOrderRepos addOrder, ILocationRepos locationRepository)
        {
            this.repository = repository;
            this.addOrderRepo = addOrder;
            this.locationRepository = locationRepository;
        }

        public IActionResult Index()
        {
            string jwtToken = HttpContext.Session.GetString("JWToken");
            Console.WriteLine("tokennya : ", jwtToken);
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
