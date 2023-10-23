using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_event_equipment")]
    public class EventEquipment : GeneralAtribute
    {
        [Column("guid_packetevent")]
        public Guid GuidPacketEvent { get; set; }
        [Column("guid_equipment")]
        public Guid GuidEquipment { get; set; }
    }
}
