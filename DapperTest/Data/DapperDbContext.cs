using DapperTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DapperTest.Data
{
    public class DapperDbContext : DbContext
    {
        public DapperDbContext(DbContextOptions<DapperDbContext> options) : 
            base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Ignore(_ => _.Employees);
            modelBuilder.Entity<Employee>().HasOne(_ => _.Company).WithMany(_ => _.Employees).HasForeignKey(_ => _.CompanyId);
        }
    }
}
