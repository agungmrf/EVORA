using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class TransactionEventRepository : GeneralRepository<TransactionEvent>, ITransactionRepository
{
    protected TransactionEventRepository(EvoraDbContext context) : base(context)
    {
    }
}