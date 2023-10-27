using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(EvoraDbContext context) : base(context)
    {
    }
    
    public Guid? getDefaultRoleEmp()
    {
        // Mengambil role user berdasarkan nama role.
        return _context.Set<Role>().FirstOrDefault(role => role.Name == "staff")?.Guid;
    }
    
    public Guid? getDefaultRoleCust()
    {
        // Mengambil role user berdasarkan nama role.
        return _context.Set<Role>().FirstOrDefault(role => role.Name == "user")?.Guid;
    }
}