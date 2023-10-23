using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EventEquipmentRepository : GeneralRepository<EventEquipment>, IEventEquipmentRepository
{
    protected EventEquipmentRepository(EvoraDbContext context) : base(context)
    {
    }
}