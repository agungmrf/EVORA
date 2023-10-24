using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class SubDistrictRepository : GeneralRepository<SubDistrict>, ISubDistrictRepository
{
    protected SubDistrictRepository(EvoraDbContext context) : base(context)
    {
    }
}