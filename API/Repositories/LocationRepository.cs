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
        var subdistrict = _context.Set<SubDistrict>().ToList();
        var district = _context.Set<District>().ToList();
        var city = _context.Set<City>().ToList();
        var province = _context.Set<Province>().ToList();

        var employeeDetails = from loc in location
                              join sub in subdistrict on loc.SubDistrictGuid equals sub.Guid
                              join dist in district on sub.DistrictGuid equals dist.Guid
                              join c in city on dist.CityGuid equals c.Guid
                              join provin in province on c.ProvinceGuid equals provin.Guid
                              select new DetailLocationDto
                              {
                                  Guid = loc.Guid,
                                  Street = loc.Street,
                                  SubDistrict = sub.Name,
                                  District = dist.Name,
                                  City = c.Name,
                                  Province = provin.Name
                              };
        DetailLocationDto getDetail = employeeDetails.FirstOrDefault(d => d.Guid == guid);
        return getDetail;
    }
}