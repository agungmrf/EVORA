using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_sub_district")]
public class SubDistrict : GeneralAtribute
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    
    [Column("district_guid")] 
    public Guid DistrictGuid { get; set; } // Foreign Key.

    // Cardinality Many To One
    public District? District { get; set; }
}