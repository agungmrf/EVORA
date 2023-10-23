using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_disctrict")]
public class District : GeneralAtribute
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    [Column("sub_disctrict_guid")]
    public string SubDisctrictGuid { get; set; }
}