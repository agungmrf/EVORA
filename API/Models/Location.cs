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
        // Cardinality Many To One
        public City? City { get; set; }
        // Cardinality One To Many
        public ICollection<TransactionEvent>? TransactionEvents { get; set; }
    }
}