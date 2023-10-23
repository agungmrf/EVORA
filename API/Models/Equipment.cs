using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    public class Equipment : GeneralAtribute
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string name { get; set; }
    }
}
