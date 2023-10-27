using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(EvoraDbContext context) : base(context)
    {
    }
    
    public IEnumerable<Guid> GetRoleGuidsByAccountGuid(Guid accountGuid)
    {
        // Mengambil account role berdasarkan account guid.
        return _context.Set<AccountRole>().Where(ar => ar.AccountGuid == accountGuid).Select(ar => ar.RoleGuid);
    }
}