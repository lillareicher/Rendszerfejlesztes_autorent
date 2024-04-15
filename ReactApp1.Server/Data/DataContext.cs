using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Models.Entities;

namespace ReactApp1.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Car> Car { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Rental> Rental { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<User> User { get; set; }

    }
}
