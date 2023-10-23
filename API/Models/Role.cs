using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Role : GeneralAtribute
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
    }
}
