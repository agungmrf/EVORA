using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.DTOs.Locations;
using API.DTOs.TransactionEvents;
using API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Repositories;

public class TransactionEventRepository : GeneralRepository<TransactionEvent>, ITransactionRepository
{
    public TransactionEventRepository(EvoraDbContext context) : base(context)
    {
    }

    public IEnumerable<TransactionDetailDto> GetAllDetailTransaction()
    {
        var location = _context.Set<Location>().ToList();
        var city = _context.Set<City>().ToList();
        var province = _context.Set<Province>().ToList();
        var transaction = _context.Set<TransactionEvent>().ToList();
        var customer = _context.Set<Customer>().ToList();
        var package = _context.Set<PackageEvent>().ToList();

        var transactionDetails = from trans in transaction
                                 join loc in location on trans.LocationGuid equals loc.Guid
                                 join c in city on loc.CityGuid equals c.Guid
                                 join provin in province on c.ProvinceGuid equals provin.Guid
                                 join cust in customer on trans.CustomerGuid equals cust.Guid
                                 join pack in package on trans.PacketEventGuid equals pack.Guid
                                 select new TransactionDetailDto
                                 {
                                     Guid=trans.Guid,
                                     CustomerGuid = cust.Guid,
                                     FirstName = cust.FirstName,
                                     LastName = cust.LastName,
                                     Email = cust.Email,
                                     Price = pack.Price,
                                     Invoice = trans.Invoice,
                                     Package = pack.Name,
                                     EventDate = trans.EventDate,
                                     Status = trans.Status,
                                     Street = loc.Street,
                                     City = c.Name
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

    public IEnumerable<TransactionDetailDto> GetByCustomer(Guid guid)
    {
        var location = _context.Set<Location>().ToList();
        var city = _context.Set<City>().ToList();
        var province = _context.Set<Province>().ToList();
        var transaction = _context.Set<TransactionEvent>().ToList();
        var customer = _context.Set<Customer>().ToList();
        var package = _context.Set<PackageEvent>().ToList();

        var transactionDetails = from trans in transaction
                                 join loc in location on trans.LocationGuid equals loc.Guid
                                 join c in city on loc.CityGuid equals c.Guid
                                 join provin in province on c.ProvinceGuid equals provin.Guid
                                 join cust in customer on trans.CustomerGuid equals cust.Guid
                                 join pack in package on trans.PacketEventGuid equals pack.Guid
                                 where trans.CustomerGuid == guid
                                 select new TransactionDetailDto
                                 {
                                     Guid = trans.Guid,
                                     CustomerGuid = cust.Guid,
                                     FirstName = cust.FirstName,
                                     LastName = cust.LastName,
                                     Email = cust.Email,
                                     Price = pack.Price,
                                     Invoice = trans.Invoice,
                                     Package = pack.Name,
                                     EventDate = trans.EventDate,
                                     Status = trans.Status,
                                     Street = loc.Street,
                                     City = c.Name
                                 };
        
        return transactionDetails;
    }

    public TransactionDetaillAllDto DetailByGuid(Guid guid)
    {

        var location = _context.Set<Location>().ToList();
        var city = _context.Set<City>().ToList();
        var province = _context.Set<Province>().ToList();
        var transaction = _context.Set<TransactionEvent>().ToList();
        var customer = _context.Set<Customer>().ToList();
        var package = _context.Set<PackageEvent>().ToList();

        var transactionDetails = from trans in transaction
                                 join loc in location on trans.LocationGuid equals loc.Guid
                                 join c in city on loc.CityGuid equals c.Guid
                                 join provin in province on c.ProvinceGuid equals provin.Guid
                                 join cust in customer on trans.CustomerGuid equals cust.Guid
                                 join pack in package on trans.PacketEventGuid equals pack.Guid
                                 select new TransactionDetaillAllDto
                                 {
                                     Guid = trans.Guid,
                                     CustomerGuid = cust.Guid,
                                     FirstName = cust.FirstName,
                                     LastName = cust.LastName,
                                     Email = cust.Email,
                                     Price = pack.Price,
                                     Invoice = trans.Invoice,
                                     Package = pack.Name,
                                     EventDate = trans.EventDate,
                                     TransactionDate = trans.TransactionDate,
                                     Status = trans.Status,
                                     Street = loc.Street,
                                     City = c.Name,
                                     Province = provin.Name,
                                     District = loc.District,
                                     SubDistrict = loc.SubDistrict,
                                     PackageGuid = pack.Guid,
                                     LocationGuid = loc.Guid
                                 };
        if (transactionDetails is null)
        {
            return new TransactionDetaillAllDto();
        }
        var getDetail = transactionDetails.FirstOrDefault(d => d.Guid == guid);
        return getDetail;
    }
}