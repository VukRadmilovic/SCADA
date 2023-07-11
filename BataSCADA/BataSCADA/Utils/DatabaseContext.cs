using BataSCADA.Models;
using Microsoft.EntityFrameworkCore;

namespace BataSCADA.Utils
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectModels;Database=BataSCADADb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().ToTable("Tags");
            modelBuilder.Entity<AnalogInput>().ToTable("AnalogInputs");
            modelBuilder.Entity<AnalogOutput>().ToTable("AnalogOutputs");
            modelBuilder.Entity<DigitalInput>().ToTable("DigitalInputs");
            modelBuilder.Entity<DigitalOutput>().ToTable("DigitalOutputs");
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AnalogInput> AnalogInputs { get; set; }
        public DbSet<AnalogOutput> AnalogOutputs { get; set; }
        public DbSet<DigitalInput> DigitalInputs { get; set; }
        public DbSet<DigitalOutput> DigitalOutputs { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<AlarmLog> AlarmLogs { get; set; }
        public DbSet<AddressValue> AddressValues { get; set; }
    }
}
