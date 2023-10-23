using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class PackageEventRepository : GeneralRepository<PackageEvent>, IPackageEventController
{
    protected PackageEventRepository(EvoraDbContext context) : base(context)
    {
    }
}