using API.Utilities.Enums;

namespace API.DTOs.TransactionEvents
{
    public class ChangeTransactionStatusDto
    {
        public Guid Guid { get; set; }
        public StatusTransaction Status { get; set; }
    }
}
