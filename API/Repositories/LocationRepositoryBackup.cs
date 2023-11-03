using API.Contracts;
using API.Data;
using API.DTOs.Locations;
using API.Models;

namespace API.Repositories;

public class LocationRepositoryBackup : GeneralRepository<Location>, ILocationRepository
{
    public LocationRepositoryBackup(EvoraDbContext context) : base(context)
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
        var transaction = _context.Set<TransactionEvent>().ToList();
        var customer = _context.Set<Customer>().ToList();

        var employeeDetails = from loc in location
                              join c in city on loc.CityGuid equals c.Guid
                              join provin in province on c.ProvinceGuid equals provin.Guid
                              join trans in transaction on loc.Guid equals trans.LocationGuid
                              join cust in customer on trans.CustomerGuid equals cust.Guid
                              select new DetailLocationDto
                              {
                                  Guid = loc.Guid,
                                  //CustomerName = cust.FirstName + " " + cust.LastName,
                                  //Email = cust.Email,
                                  //Invoice = trans.Invoice,
                                  //Street = loc.Street,
                                  City = c.Name,
                                  Province = provin.Name
                              };
        return employeeDetails.ToList();
    }
}