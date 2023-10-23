using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class PacketEvent : GeneralAtribute
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string name { get; set; }
        [Column("capacity", TypeName = "int")]
        public string capacity { get; set; }
        [Column("price", TypeName = "decimal")]
        public string price { get; set; }
    }
}
