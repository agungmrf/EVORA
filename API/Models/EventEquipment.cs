using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class EventEquipment : GeneralAtribute
    {
        [Column("guid_packetevent")]
        public Guid GuidPacketEvent { get; set; }
        [Column("guid_equipment")]
        public Guid GuidEquipment { get; set; }
    }
}
