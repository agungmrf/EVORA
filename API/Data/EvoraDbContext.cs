using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class EvoraDbContext : DbContext
    {
        public EvoraDbContext(DbContextOptions<EvoraDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EventEquipment> EventEquipments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PackageEvent> PackageEvents { get; set; }
        public DbSet<TransactionEvent> TransactionEvents { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
