using API.Contracts;
using API.DTOs.TransactionEvents;
using API.Utilities.Enums;
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers.Staff
{
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
