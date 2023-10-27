using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class SubDistrictRepository : GeneralRepository<SubDistrict>, ISubDistrictRepository
{
    public SubDistrictRepository(EvoraDbContext context) : base(context)
    {
    }
}