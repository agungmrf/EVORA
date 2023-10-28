using API.Contracts;
using API.Data;
using API.DTOs.Locations;
using API.Models;

namespace API.Repositories;

public class CityRepository : GeneralRepository<City>, ICityRepository
{
    public CityRepository(EvoraDbContext context) : base(context)
    {

    }

    public IEnumerable<City> GetByProvinceGuid(Guid guid)
    {
        var city = _context.Set<City>().Where(provinsi => provinsi.ProvinceGuid == guid);
        return city;
    }
}