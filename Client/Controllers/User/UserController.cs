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
using API.Utilities.Enums;
using System.IO.Pipelines;

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
        private readonly ITransactionRepos transactionRepository;
        public UserController(IUserRepository repository, IAddOrderRepos addOrder,
            ILocationRepos locationRepository, IAccountRepos accountRepository, 
            IGetCustomerRepository getcustomerRepository, ITransactionRepos transactionRepository)
        {
            this.repository = repository;
            this.addOrderRepo = addOrder;
            this.locationRepository = locationRepository;
            this.accountRepository = accountRepository;
            this.getcustomerRepository = getcustomerRepository;
            this.transactionRepository = transactionRepository;
        }

        public async Task<IActionResult> Index()
        {
            string jwtToken = HttpContext.Session.GetString("JWToken");
            var dataUser = await accountRepository.GetClaims(jwtToken);
            if (dataUser != null)
            {
                HttpContext.Session.SetString("Name", dataUser.Data.Name);
                var email = dataUser.Data.Email;
                ViewBag.Name = dataUser.Data.Name;
                ViewBag.Email = email;
                var getCust = getcustomerRepository.GetbyEmail(email);
                var customerGuid = getCust.Result.Data.Guid;
                var getTransaction = transactionRepository.GetbyGuid(customerGuid);
                if (getTransaction != null)
                {
                    var data = getTransaction.Result.Data;
                    return View(data);
                }
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
            var price = dataPacket.Price;
            var priceFormat = price.ToString("C", new System.Globalization.CultureInfo("id-ID"));
            ViewBag.Price = priceFormat;
            ;

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
        public async Task<IActionResult> Checkout(Guid id)
        {
            var getTransaction = await transactionRepository.DetailbyGuid(id);
            if (getTransaction != null)
            {
                var data = getTransaction.Data;
                return View(data);
            }
            return View();
        }
        public async Task<IActionResult> Checkouts(Guid id)
        {
            var getTransaction = await transactionRepository.TransactionbyGuid(id);
            var datatransaction = getTransaction.Data;
            datatransaction.Status = (StatusTransaction)1;
            var toUpdateTransaction = await transactionRepository.ApprovePayment(id, datatransaction);
            if (toUpdateTransaction != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> CancelPayment(Guid id)
        {
            var getTransaction = await transactionRepository.TransactionbyGuid(id);
            var datatransaction = getTransaction.Data;
            datatransaction.Status = (StatusTransaction)0;
            var toUpdateTransaction = await transactionRepository.ApprovePayment(id, datatransaction);
            if (toUpdateTransaction != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Histories()
        {
            string jwtToken = HttpContext.Session.GetString("JWToken");
            var dataUser = await accountRepository.GetClaims(jwtToken);
            if (dataUser != null)
            {
                var email = dataUser.Data.Email;
                var getCust = getcustomerRepository.GetbyEmail(email);
                var customerGuid = getCust.Result.Data.Guid;
                var getTransaction = transactionRepository.GetbyGuid(customerGuid);
                if(getTransaction  != null)
                {
                    var data = getTransaction.Result.Data;
                    return View(data);
                }
            }
            return View();
        }
    }
}
