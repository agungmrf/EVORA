using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class ProvinceRepository : GeneralRepository<Province>, IProvinceRepository
{
    protected ProvinceRepository(EvoraDbContext context) : base(context)
    {
    }
}