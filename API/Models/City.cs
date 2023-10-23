using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_city")]
public class City : GeneralAtribute
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    [Column("province_guid")]
    public string ProvinceGuid { get; set; }
    [Column("disctrict_guid")]
    public string DisctrictGuid { get; set; }
}