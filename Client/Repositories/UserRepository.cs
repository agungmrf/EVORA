using API.Contracts;
using API.DTOs.Employees;
using API.DTOs.PackageEvents;
using API.Models;
using Client.Contracts;
using Client.Repository;

namespace Client.Repositories
{
    public class UserRepository : GeneralRepository<PackageEventDto, Guid>, IUserRepository
    {
        public UserRepository(string request = "PackageEvent/") : base(request)
        {

        }
    }
}