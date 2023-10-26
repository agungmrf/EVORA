using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers.DashboardAdmin
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Employees()
        {
            return View();
        }
        public IActionResult Customers()
        {
            return View();
        }
        public IActionResult Locations()
        {
            return View();
        }
        public IActionResult Packages()
        {
            return View();
        }
        public IActionResult Transactions()
        {
            return View();
        }
    }
}
