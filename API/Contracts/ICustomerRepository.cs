using API.Models;

namespace API.Contracts;

public interface ICustomerRepository : IGeneralRepository<Customer>
{
    Customer? GetByEmail(string email);
}