using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_location")]
    public class Location : GeneralAtribute
    {
        [Column("street", TypeName = "nvarchar(100)")]
        public string Street { get; set; }
        
        [Column("sub_district_guid")]
        public Guid SubDistrictGuid { get; set; }

        public SubDistrict? SubDistrict { get; set; } // Cardinality Many To One
        public ICollection<TransactionEvent>? TransactionEvents { get; set; } // Cardinality One To Many

        //[Column("city_guid")] public Guid CityGuid { get; set; }
        //public City? City { get; set; }
    }
}