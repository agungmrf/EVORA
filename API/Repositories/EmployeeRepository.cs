using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        protected EmployeeRepository(EvoraDbContext context) : base(context)
        {
        }
    }
}
