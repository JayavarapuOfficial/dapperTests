using DapperTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperTest.Data
{
    public class DapperDbContext : DbContext
    {
        public DapperDbContext(DbContextOptions<DapperDbContext> options) : 
            base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
    }
}
