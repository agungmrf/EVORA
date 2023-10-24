using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_city")]
public class City : GeneralAtribute
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    [Column("province_guid")]
    public Guid ProvinceGuid { get; set; }
    [Column("disctrict_guid")]
    public Guid DisctrictGuid { get; set; }
    // Cardinality One To Many
    public ICollection<Location>? Location { get; set; }
    // Cardinality Many To One
    public Province? Province { get; set; }
    public District? District { get; set; }
}