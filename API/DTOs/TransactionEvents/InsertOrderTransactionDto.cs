using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.TransactionEvents;

public class InsertOrderTransactionDto
{
    //public string Invoice { get; set; }
    //public StatusTransaction Status { get; set; }
    //public DateTime TransactionDate { get; set; }
    //public DateTime CreatedDate { get; set; }
    //public DateTime ModifiedDate { get; set; }
    //public Guid CustomerGuid { get; set; }
    //public Guid PackageGuid { get; set; }
    //public Guid LocationGuid { get; set; }
    public Guid GuidCustomer { get; set; }
    public Guid PackageGuid { get; set; }
    public DateTime EventDate { get; set; }
    public string Street { get; set; }
    public string Disctrict { get; set; }
    public string SubDistrict { get; set; }
    public Guid ProvinceGuid { get; set; }
    public Guid CityGuid { get; set; }
    

    //public static implicit operator TransactionEvent(InsertOrderTransactionDto insertOrder)
    //{
    //    return new TransactionEvent
    //    {
    //        Guid = new Guid(),
    //        Invoice = insertOrder.Invoice,
    //        EventDate = insertOrder.EventDate,
    //        Status = (StatusTransaction)2,
    //        TransactionDate = DateTime.Now,
    //        CustomerGuid = insertOrder.CustomerGuid,
    //        PacketEventGuid = insertOrder.PackageGuid,
    //        LocationGuid = insertOrder.LocationGuid,
    //        CreatedDate = DateTime.Now,
    //        ModifiedDate = DateTime.Now
    //    };
    //}
}