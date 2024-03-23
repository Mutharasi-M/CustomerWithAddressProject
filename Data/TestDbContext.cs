using Microsoft.EntityFrameworkCore;

namespace Test.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<AddressTable> Address { get; set; }
    }
}
