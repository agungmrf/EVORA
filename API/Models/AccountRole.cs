using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_account_roles")]
    public class AccountRole : GeneralAtribute
    {
        [Column("role_guid")]
        public Guid RoleGuid { get; set; }
        [Column("account_guid")]
        public Guid AccountGuid { get; set; }
        // Cardinality Many To One
        public Role? Role { get; set; }
        public Account? Account { get; set; }
    }
}