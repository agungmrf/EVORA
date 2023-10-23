using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_transaction_event")]
    public class TransactionEvent : GeneralAtribute
    {
        [Column("customer_guid")]
        public Guid CustomerGuid { get; set; }
        [Column("package_event_guid")]
        public Guid PacketEventGuid { get; set; }
        [Column("location_guid")]
        public Guid LocationGuid { get; set; }
        [Column("invoice")]
        public string Invoice { get; set; }
        [Column("event_date")]
        public DateTime EventDate { get; set; }
        [Column("status", TypeName = "int")]
        public string Status { get; set; }
        [Column("transaction_date")]
        public DateTime TransactionDate { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }

    }
}
