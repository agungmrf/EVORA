using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    
    public class GeneralAtribute
    {
        [Key] [Column("guid")] public Guid Guid { get; set; } // Primary Key.
    }
}
