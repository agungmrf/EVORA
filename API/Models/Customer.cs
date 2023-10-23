using System.ComponentModel.DataAnnotations.Schema;
using API.Utilities.Enums;

namespace API.Models
{
    [Table("tb_m_customer")]
    public class Customer : GeneralAtribute
    {
        [Column("first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Column("last_name", TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }
        [Column("email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column("phone_number", TypeName = "nvarchar(25)")]
        public string PhoneNumber { get; set; }
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }
        [Column("gender")] 
        public GenderLevel Gender { get; set; }
    }
}
