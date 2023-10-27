using API.Contracts;
using API.Data;
using API.DTOs.Locations;
using API.Models;

namespace API.Repositories;

public class LocationRepository : GeneralRepository<Location>, ILocationRepository
{
    public LocationRepository(EvoraDbContext context) : base(context)
    {

    }

    public DetailLocationDto? GetDetailLocation(Guid guid)
    {
        var location = _context.Set<Location>().ToList();
        var city = _context.Set<City>().ToList();
        var province = _context.Set<Province>().ToList();

        var employeeDetails = from loc in location
                              join c in city on loc.CityGuid equals c.Guid
                              join provin in province on c.ProvinceGuid equals provin.Guid
                              select new DetailLocationDto
                              {
                                  Guid = loc.Guid,
                                  District = loc.District,
                                  SubDistrict = loc.SubDistrict,
                                  City = c.Name,
                                  Province = provin.Name
                              };
        DetailLocationDto getDetail = employeeDetails.FirstOrDefault(d => d.Guid == guid);
        return getDetail;
    }

    IEnumerable<DetailLocationDto> ILocationRepository.GetAllDetailLocation()
    {
        var location = _context.Set<Location>().ToList();
        var city = _context.Set<City>().ToList();
        var province = _context.Set<Province>().ToList();

        var employeeDetails = from loc in location
                              join c in city on loc.CityGuid equals c.Guid
                              join provin in province on c.ProvinceGuid equals provin.Guid
                              select new DetailLocationDto
                              {
                                  Guid = loc.Guid,
                                  District = loc.District,
                                  SubDistrict = loc.SubDistrict,
                                  City = c.Name,
                                  Province = provin.Name
                              };
        return employeeDetails.ToList();
    }
}