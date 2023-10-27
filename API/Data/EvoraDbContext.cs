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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PackageEvent> PackageEvents { get; set; }
        public DbSet<TransactionEvent> TransactionEvents { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Province> Province { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasIndex(e => e.Nik).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.PhoneNumber).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(e => e.PhoneNumber).IsUnique();

            // One Role has many AccountRoles
            modelBuilder.Entity<Role>()
                .HasMany(arole => arole.AccountRoles)
                .WithOne(ar => ar.Role)
                .HasForeignKey(arole => arole.RoleGuid)
                .OnDelete(DeleteBehavior.Restrict);
            // Many AccountRoles has one Account
            modelBuilder.Entity<AccountRole>()
                .HasOne(arole => arole.Account)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(arole => arole.AccountGuid)
                .OnDelete(DeleteBehavior.Restrict);
            // One City has many location
            modelBuilder.Entity<City>()
                .HasMany(loc => loc.Location)
                .WithOne(city => city.City)
                .HasForeignKey(loc => loc.CityGuid)
                .OnDelete(DeleteBehavior.Restrict);
            // One Province has many City
            modelBuilder.Entity<Province>()
                .HasMany(city => city.Cities)
                .WithOne(prov => prov.Province)
                .HasForeignKey(city => city.ProvinceGuid)
                .OnDelete(DeleteBehavior.Restrict);
<<<<<<< Updated upstream
            // One District has many City
            modelBuilder.Entity<District>()
                .HasMany(city => city.Cities)
                .WithOne(dis => dis.District)
                .HasForeignKey(city => city.DisctrictGuid)
                .OnDelete(DeleteBehavior.Restrict);
            // One District has many Subdistrict
            modelBuilder.Entity<SubDistrict>()
                .HasOne(sub => sub.District)
                .WithMany(dis => dis.SubDistricts)
                .HasForeignKey(dis => dis.DisctrictGuid)
=======
            // One City has many location
            modelBuilder.Entity<City>()
                .HasMany(dist => dist.Location)
                .WithOne(city => city.City)
                .HasForeignKey(dist => dist.CityGuid)
>>>>>>> Stashed changes
                .OnDelete(DeleteBehavior.Restrict);
            // One location has many transaction
            modelBuilder.Entity<Location>()
                .HasMany(transaction => transaction.TransactionEvents)
                .WithOne(loc => loc.Location)
                .HasForeignKey(transaction => transaction.LocationGuid)
                .OnDelete(DeleteBehavior.Restrict);
            // One package has many transaction
            modelBuilder.Entity<PackageEvent>()
                .HasMany(transaction => transaction.TransactionEvents)
                .WithOne(packet => packet.PackageEvent)
                .HasForeignKey(transaction => transaction.PacketEventGuid)
                .OnDelete(DeleteBehavior.Restrict);
            // One customer has many transaction
            modelBuilder.Entity<Customer>()
                .HasMany(transaction => transaction.TransactionEvents)
                .WithOne(user => user.Customer)
                .HasForeignKey(transaction => transaction.CustomerGuid)
                .OnDelete(DeleteBehavior.Restrict);
<<<<<<< Updated upstream

            modelBuilder.Entity<Account>()
                .HasKey(a => a.Guid);

=======
            // One Account has one Employee
>>>>>>> Stashed changes
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Guid);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Guid);

            // One Account has one Employee
            modelBuilder.Entity<Account>()
                .HasOne(emp => emp.Employee)
                .WithOne(account => account.Account)
                .HasForeignKey<Account>(e => e.Guid);
            // One Account has one Customer
            modelBuilder.Entity<Account>()
                .HasOne(user => user.Customer)
                .WithOne(account => account.Account)
                .HasForeignKey<Account>(c => c.Guid);
        }
    }
}
