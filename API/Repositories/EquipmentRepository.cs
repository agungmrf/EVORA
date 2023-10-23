using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EquipmentRepository : GeneralRepository<Equipment>, IEquipmentRepository
{
    protected EquipmentRepository(EvoraDbContext context) : base(context)
    {
    }
}