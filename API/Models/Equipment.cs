using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("tb_m_equipment")]
    public class Equipment : GeneralAtribute
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string name { get; set; }
    }
}
