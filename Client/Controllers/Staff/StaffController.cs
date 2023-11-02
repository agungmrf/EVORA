using API.DTOs.TransactionEvents;
using API.Utilities.Enums;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.Staff
{
    [Authorize(Roles = "Staff,Admin")]
    public class StaffController : Controller
    {
        private readonly ITransactionRepos transactionRepository;

        public StaffController(ITransactionRepos transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var getTransaction = await transactionRepository.DetailAll();
            var cekData = getTransaction.Data;
            if (cekData != null)
            {
                return View(cekData);
            }
            return View();
        }

        public async Task<IActionResult> StatusApprove(Guid id)
        {
            var updateStatus = new ChangeTransactionStatusDto
            {
                Status = (StatusTransaction)3,
                Guid = id
            };
            var updateTransaction = await transactionRepository.ChangeStatus(updateStatus);
            var cekData = updateTransaction.Data;
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> StatusDecline(Guid id)
        {
            var updateStatusDec = new ChangeTransactionStatusDto
            {
                Status = 0,
                Guid = id
            };
            var updateTransaction = await transactionRepository.ChangeStatus(updateStatusDec);
            var cekData = updateTransaction.Data;
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ListEvent()
        {
            return View();
        }
        [Route("events/get")]
        public async Task<IActionResult> GetEvents()
        {
            var getTransaction = await transactionRepository.DetailAll();
            var cekData = getTransaction.Data;
            var eventList = cekData.Select(e => new
            {
                id = e.Guid,
                title = e.Package+"-"+e.FirstName,
                start = e.EventDate.ToString("yyyy-MM-ddTHH:mm:ss"),
            // end = e.EventDate.ToString("yyyy-MM-ddTHH:mm:ss"),
        });
            return new JsonResult(eventList);
        }
        public IActionResult Packages()
        {
            return View();
        }
        public IActionResult Histories()
        {
            return View();
        }
    }
}
