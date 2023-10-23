using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Employee : GeneralAtribute
    {
        [Column("nik", TypeName = "nchar(12)")]
        public string Nik { get; set; }
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
        [Column("hiring_date")]
        public DateTime HiringDate { get; set; }
        
    }
}
