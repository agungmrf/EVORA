using API.Models;

namespace API.Contracts;

public interface IAccountRoleRepository : IGeneralRepository<AccountRole>
{
    IEnumerable<Guid> GetRoleGuidsByAccountGuid(Guid accountGuid);
}