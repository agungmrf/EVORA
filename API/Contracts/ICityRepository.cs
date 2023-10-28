using API.DTOs.Locations;
using API.Models;

namespace API.Contracts;

public interface ICityRepository : IGeneralRepository<City>
{
    IEnumerable<City> GetByProvinceGuid(Guid guid);
}