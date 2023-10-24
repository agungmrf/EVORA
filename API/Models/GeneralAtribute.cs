using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    
    public class GeneralAtribute
    {
        [Key, Column("guid", TypeName = "nvarchar(128)")]
        public Guid Guid { get; set; }
    }
}
