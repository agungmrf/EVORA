using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_transaction_event")]
    public class TransactionEvent : GeneralAtribute
    {
        [Column("guid_customer")]
        public Guid GuidCustomer { get; set; }
        [Column("guid_packetevent")]
        public Guid GuidPacketEvent { get; set; }
        [Column("guid_location")]
        public Guid GuidLocation { get; set; }
        [Column("event_date")]
        public DateTime EventDate { get; set; }
        [Column("transaction_date")]
        public DateTime TransactionDate { get; set; }
        [Column("status", TypeName = "int")]
        public string Status { get; set; }

    }
}
