using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_location")]
    public class Location : GeneralAtribute
    {
        [Column("city_guid")]
        public string CityGuid { get; set; }
        [Column("street", TypeName = "nvarchar(100)")]
        public string Street { get; set; }
    }
}
