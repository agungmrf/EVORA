using API.DTOs.Locations;
using API.Models;

namespace API.Contracts;

public interface ILocationRepository : IGeneralRepository<Location>
{
    IEnumerable<DetailLocationDto> GetAllDetailLocation();

    DetailLocationDto? GetDetailLocation(Guid guid);
}