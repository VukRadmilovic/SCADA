using BataSCADA.Models;
using Microsoft.EntityFrameworkCore;

namespace BataSCADA.Utils
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()  { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectModels;Database=BataSCADADb");
        }
        public DbSet<User>? Users { get; set; }
    }
}
