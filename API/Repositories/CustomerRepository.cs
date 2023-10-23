using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class CustomerRepository : GeneralRepository<Customer>, ICustomerRepository
{
    protected CustomerRepository(EvoraDbContext context) : base(context)
    {
    }
}