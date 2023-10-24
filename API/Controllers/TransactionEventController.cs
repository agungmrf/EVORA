using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class TransactionEventController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionEventController(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
}