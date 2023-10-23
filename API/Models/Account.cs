using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Account : GeneralAtribute
    {
        [Column("password", TypeName = "nvarchar(100)")]
        public string Password { get; set; }
        [Column("otp")]
        public int Otp { get; set; }
        [Column("is_used")]
        public bool IsUsed { get; set; }
        [Column("expired_date")]
        public DateTime ExpiredDate { get; set; }
    }
}
