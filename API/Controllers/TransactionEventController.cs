using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TransactionEventController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionEventController(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
}