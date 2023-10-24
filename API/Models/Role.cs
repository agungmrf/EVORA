using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_role")]
    public class Role : GeneralAtribute
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        // Cardinality One To Many
        public ICollection<AccountRole>? AccountRoles { get; set; }
    }
}
