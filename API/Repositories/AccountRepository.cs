using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    protected AccountRepository(EvoraDbContext context) : base(context)
    {
    }
}