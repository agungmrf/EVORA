using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_account")]
    public class Account : GeneralAtribute
    {
        [Column("otp")]
        public int Otp { get; set; }
        [Column("is_used")]
        public bool IsUsed { get; set; }
        [Column("expired_date")]
        public DateTime ExpiredDate { get; set; }
        [Column("password", TypeName = "nvarchar(100)")]
        public string Password { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
        // Cardinality One To One
        public Employee? Employee { get; set; }
        public Customer? Customer { get; set; }
        // Cardinality One To Many
        public ICollection<AccountRole>? AccountRoles { get; set; }
    }
}
