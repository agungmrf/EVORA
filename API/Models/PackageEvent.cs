using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{ 
    [Table("tb_m_package_event")]
    public class PackageEvent : GeneralAtribute
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column("capacity", TypeName = "int")]
        public string Capacity { get; set; }
        [Column("description", TypeName = "nvarchar(max)")]
        public string Desciption { get; set; }
        [Column("price", TypeName = "money")]
        public string Price { get; set; }
    }
}
