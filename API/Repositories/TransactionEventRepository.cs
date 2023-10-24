using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class TransactionEventRepository : GeneralRepository<TransactionEvent>, ITransactionRepository
{
    public TransactionEventRepository(EvoraDbContext context) : base(context)
    {
    }
}