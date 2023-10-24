using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class LocationRepository : GeneralRepository<Location>, ILocationRepository
{
    public LocationRepository(EvoraDbContext context) : base(context)
    {
    }
}