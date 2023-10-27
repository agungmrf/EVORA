using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class CityRepository : GeneralRepository<City>, ICityRepository
{
    public CityRepository(EvoraDbContext context) : base(context)
    {
    }
}