using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class CustomerRepository : GeneralRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(EvoraDbContext context) : base(context)
    {
    }
    
    public Customer? GetByEmail(string email)
    {
        // Mengambil data dari database dengan model Employee, kemudian diambil data yang emailnya mengandung email yang diberikan
        return _context.Set<Customer>().SingleOrDefault(c => c.Email.Contains(email));
    }
}