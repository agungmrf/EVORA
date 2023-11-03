using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(EvoraDbContext context) : base(context)
    {
    }

    public IEnumerable<AccountRole> GetRoleGuidsByAccountGuid(Guid? accountGuid)
    {
        // Mengambil account role berdasarkan account guid.
        var entity = _context.Set<AccountRole>().Where(ar => ar.AccountGuid == accountGuid).ToList();
        return entity;
    }
}