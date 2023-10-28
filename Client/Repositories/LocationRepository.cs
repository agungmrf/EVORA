using API.DTOs.Locations;
using API.DTOs.PackageEvents;
using Client.Contracts;
using Client.Repository;

namespace Client.Repositories
{
    public class LocationRepository : GeneralRepository<LocationDto, Guid>, ILocationRepos
    {
        public LocationRepository(string request = "Location/") : base(request)
        {

        }
    }
}
