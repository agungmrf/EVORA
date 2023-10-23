using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Location : GeneralAtribute
    {
        [Column("street", TypeName = "nvarchar(100)")]
        public string Street { get; set; }
        [Column("village", TypeName = "nvarchar(100)")]
        public string Village { get; set; } // kelurahan
        [Column("district ", TypeName = "nvarchar(100)")]
        public string District { get; set; } // kecamatan
        [Column("city", TypeName = "nvarchar(100)")]
        public string City { get; set; } // kota
        [Column("province", TypeName = "nvarchar(100)")]
        public string Province { get; set; } // provinsi
    }
}
