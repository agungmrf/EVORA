using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class LocationRepository : GeneralRepository<Location>, ILocationRepository
{
    protected LocationRepository(EvoraDbContext context) : base(context)
    {
    }
}