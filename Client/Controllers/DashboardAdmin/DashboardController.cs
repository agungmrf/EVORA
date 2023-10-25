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
<<<<<<< Updated upstream
        public IActionResult Employee()
        {
            return View();
        }
=======
>>>>>>> Stashed changes
    }
}
