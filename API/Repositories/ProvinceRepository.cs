using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class ProvinceRepository : GeneralRepository<Province>, IProvinceRepository
{
    public ProvinceRepository(EvoraDbContext context) : base(context)
    {
    }
}