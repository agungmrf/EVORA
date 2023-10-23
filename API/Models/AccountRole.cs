using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class AccountRole : GeneralAtribute
    {
        [Column("guid_role")]
        public Guid GuidRole { get; set; }
        [Column("guid_account")]
        public Guid GuidAccount { get; set; }
    }
}
