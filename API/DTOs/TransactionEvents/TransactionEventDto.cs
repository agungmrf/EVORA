using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.TransactionEvents;

public class TransactionEventDto
{
    public Guid Guid { get; set; }
    public string Invoice { get; set; }
    public DateTime EventDate { get; set; }
    public StatusTransaction Status { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Guid CustomerGuid { get; set; }
    public Guid PackageGuid { get; set; }
    public Guid LocationGuid { get; set; }
    
    public static explicit operator TransactionEventDto(TransactionEvent transactionEvent)
    {
        return new TransactionEventDto
        {
            Guid = transactionEvent.Guid,
            Invoice = transactionEvent.Invoice,
            EventDate = transactionEvent.EventDate,
            Status = transactionEvent.Status,
            TransactionDate = transactionEvent.TransactionDate,
            CreatedDate = transactionEvent.CreatedDate,
            ModifiedDate = transactionEvent.ModifiedDate,
            CustomerGuid = transactionEvent.CustomerGuid,
            PackageGuid = transactionEvent.PacketEventGuid,
            LocationGuid = transactionEvent.LocationGuid
        };
    }
    
    public static implicit operator TransactionEvent(TransactionEventDto transactionEventDto)
    {
        return new TransactionEvent
        {
            Guid = transactionEventDto.Guid,
            Invoice = transactionEventDto.Invoice,
            EventDate = transactionEventDto.EventDate,
            Status = transactionEventDto.Status,
            TransactionDate = transactionEventDto.TransactionDate,
            CustomerGuid = transactionEventDto.CustomerGuid,
            PacketEventGuid = transactionEventDto.PackageGuid,
            LocationGuid = transactionEventDto.LocationGuid,
            ModifiedDate = transactionEventDto.ModifiedDate
        };
    }
}