using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_district")]
public class District : GeneralAtribute
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    
    [Column("city_guid")] 
    public Guid CityGuid { get; set; } // Foreign Key.

    public City? City { get; set; } // many to one
    public ICollection<SubDistrict>? SubDistricts { get; set; } // Cardinality One To Many
}