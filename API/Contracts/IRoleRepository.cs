using API.Models;

namespace API.Contracts;

public interface IRoleRepository : IGeneralRepository<Role>
{
    Guid? getDefaultRoleEmp(string role);

    Guid? getDefaultRoleCust();
}