using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_account_roles")]
    public class AccountRole : GeneralAtribute
    {
        [Column("guid_role")]
        public Guid GuidRole { get; set; }
        [Column("guid_account")]
        public Guid GuidAccount { get; set; }
    }
}
