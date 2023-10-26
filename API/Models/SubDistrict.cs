using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_sub_district")]
public class SubDistrict : GeneralAtribute
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    
    [Column("district_guid")] 
    public Guid DistrictGuid { get; set; } // Foreign Key.
    public District? District { get; set; } // Cardinality Many To One    
    public ICollection<Location>? Locations { get; set; } // Cardinality One To Many
}