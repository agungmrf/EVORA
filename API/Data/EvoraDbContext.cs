﻿using API.Models;
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
                .OnDelete(DeleteBehavior.Restrict)
                .OnDelete(DeleteBehavior.Cascade);
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
            // One City has many location
            modelBuilder.Entity<City>()
                .HasMany(dist => dist.Location)
                .WithOne(city => city.City)
                .HasForeignKey(dist => dist.CityGuid)
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

            modelBuilder.Entity<Account>()
                .HasKey(a => a.Guid);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Guid);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Guid);

            // One Account has one Employee
            modelBuilder.Entity<Employee>()
                .HasOne(emp => emp.Account)
                .WithOne(account => account.Employee)
                .HasForeignKey<Employee>(e => e.AccountGuid)
                .OnDelete(DeleteBehavior.Cascade);
            // One Account has one Customer
            modelBuilder.Entity<Customer>()
                .HasOne(user => user.Account)
                .WithOne(account => account.Customer)
                .HasForeignKey<Customer>(c => c.AccountGuid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
