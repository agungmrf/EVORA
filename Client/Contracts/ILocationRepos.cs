using API.DTOs.Locations;

namespace Client.Contracts
{
    public interface ILocationRepos : IRepository<LocationDto, Guid>
    {
    }
}
