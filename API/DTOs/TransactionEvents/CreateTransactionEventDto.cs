using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.TransactionEvents;

public class CreateTransactionEventDto
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
    
    public static implicit operator TransactionEvent(CreateTransactionEventDto createTransactionEventDto)
    {
        return new TransactionEvent
        {
            Guid = createTransactionEventDto.Guid,
            Invoice = createTransactionEventDto.Invoice,
            EventDate = createTransactionEventDto.EventDate,
            Status = createTransactionEventDto.Status,
            TransactionDate = createTransactionEventDto.TransactionDate,
            CustomerGuid = createTransactionEventDto.CustomerGuid,
            PacketEventGuid = createTransactionEventDto.PackageGuid,
            LocationGuid = createTransactionEventDto.LocationGuid,
            CreatedDate = createTransactionEventDto.CreatedDate,
            ModifiedDate = createTransactionEventDto.ModifiedDate
        };
    }
}