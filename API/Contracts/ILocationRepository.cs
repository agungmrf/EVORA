using API.DTOs.Locations;
using API.Models;

namespace API.Contracts;

public interface ILocationRepository : IGeneralRepository<Location>
{
    DetailLocationDto? GetDetailLocation(Guid guid);
}