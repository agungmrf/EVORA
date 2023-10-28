using API.DTOs.PackageEvents;
using API.Models;

namespace Client.Contracts
{
    public interface IUserRepository : IRepository<PackageEventDto, Guid>
    {
    }
}
