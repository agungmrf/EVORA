using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class DistrictRepository : GeneralRepository<District>, IDistrictRepository
{
    public DistrictRepository(EvoraDbContext context) : base(context)
    {
    }
}