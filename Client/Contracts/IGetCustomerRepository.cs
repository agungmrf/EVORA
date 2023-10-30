using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.DTOs.PackageEvents;
using API.Models;
using API.Utilities.Handler;
using Client.Models;

namespace Client.Contracts
{
    public interface IGetCustomerRepository : IRepository<CustomerDto, Guid>
    {
        Task<ResponseOKHandler<CustomerDto>> GetbyEmail(string email);
    }
}
