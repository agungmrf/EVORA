using API.Contracts;
using API.Data;
using API.DTOs.Locations;
using API.DTOs.TransactionEvents;
using API.Models;

namespace API.Repositories;

public class TransactionEventRepository : GeneralRepository<TransactionEvent>, ITransactionRepository
{
    public TransactionEventRepository(EvoraDbContext context) : base(context)
    {
    }

    public IEnumerable<TransactionDetailDto> GetAllDetailTransaction()
    {
        var location = _context.Set<Location>().ToList();
        var subdistrict = _context.Set<SubDistrict>().ToList();
        var district = _context.Set<District>().ToList();
        var city = _context.Set<City>().ToList();
        var province = _context.Set<Province>().ToList();
        var transaction = _context.Set<TransactionEvent>().ToList();
        var customer = _context.Set<Customer>().ToList();
        var package = _context.Set<PackageEvent>().ToList();

        var transactionDetails = from loc in location
                              join sub in subdistrict on loc.SubDistrictGuid equals sub.Guid
                              join dist in district on sub.DistrictGuid equals dist.Guid
                              join c in city on dist.CityGuid equals c.Guid
                              join provin in province on c.ProvinceGuid equals provin.Guid
                              join trans in transaction on loc.Guid equals trans.LocationGuid
                              join cust in customer on trans.CustomerGuid equals cust.Guid
                              join pack in package on trans.PacketEventGuid equals pack.Guid
                                 select new TransactionDetailDto
                              {
                                  Guid = loc.Guid,
                                  Email = cust.Email,
                                  Invoice = trans.Invoice,
                                  EventDate = trans.EventDate,
                                  TransactionDate = trans.TransactionDate,
                                  Status = trans.Status,
                                  Package = pack.Name,
                                  City = c.Name,
                                  
                               };
        return transactionDetails.ToList();
    }

    public string GetLastTransactionByYear(string year)
    {
        var lastInvoiceYear = _context.Set<TransactionEvent>().ToList()
            .OrderBy(trans => trans.Invoice)
            .Where(t => t.Invoice.Contains(year)).LastOrDefault()?.Invoice;
        
        return lastInvoiceYear;
    }
<<<<<<< Updated upstream
=======

    public IEnumerable<TransactionDetailDto> GetAllDetailTransaction()
    {
        var location = _context.Set<Location>().ToList();
        var city = _context.Set<City>().ToList();
        var province = _context.Set<Province>().ToList();
        var transaction = _context.Set<TransactionEvent>().ToList();
        var customer = _context.Set<Customer>().ToList();
        var package = _context.Set<PackageEvent>().ToList();

        var transactionDetails = from loc in location
                              join c in city on loc.CityGuid equals c.Guid
                              join provin in province on c.ProvinceGuid equals provin.Guid
                              join trans in transaction on loc.Guid equals trans.LocationGuid
                              join cust in customer on trans.CustomerGuid equals cust.Guid
                              join pack in package on trans.PacketEventGuid equals pack.Guid
                                 select new TransactionDetailDto
                              {
                                  Guid = loc.Guid,
                                  Email = cust.Email,
                                  Invoice = trans.Invoice,
                                  EventDate = trans.EventDate,
                                  TransactionDate = trans.TransactionDate,
                                  Status = trans.Status,
                                  Package = pack.Name,
                                  City = c.Name,
                               };
        return transactionDetails.ToList();
    }

    public string GetLastTransactionByYear(string year)
    {
        var lastInvoiceYear = _context.Set<TransactionEvent>().ToList()
            .OrderBy(trans => trans.Invoice)
            .Where(t => t.Invoice.Contains(year)).LastOrDefault()?.Invoice;
        
        return lastInvoiceYear;
    }
>>>>>>> Stashed changes
}