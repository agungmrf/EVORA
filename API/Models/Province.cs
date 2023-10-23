using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_province")]
public class Province : GeneralAtribute
{
    [Column("name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
}