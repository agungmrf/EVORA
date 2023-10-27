using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_location")]
    public class Location : GeneralAtribute
    {
        [Column("city_guid")]
        public Guid CityGuid { get; set; }
        [Column("street", TypeName = "nvarchar(100)")]
        public string Street { get; set; }
<<<<<<< Updated upstream
        // Cardinality Many To One
        public City? City { get; set; }
        // Cardinality One To Many
        public ICollection<TransactionEvent>? TransactionEvents { get; set; }
=======
        [Column("district", TypeName = "nvarchar(100)")]
        public string District { get; set; }
        [Column("sub_district", TypeName = "nvarchar(100)")]
        public string SubDistrict { get; set; }

        [Column("city_guid")]
        public Guid? CityGuid { get; set; }
        public City? City { get; set; } // Cardinality Many To One
        public ICollection<TransactionEvent>? TransactionEvents { get; set; } // Cardinality One To Many
>>>>>>> Stashed changes
    }
}
