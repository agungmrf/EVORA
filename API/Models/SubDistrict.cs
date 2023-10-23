using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_sub_disctrict")]
public class SubDistrict : GeneralAtribute
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
}