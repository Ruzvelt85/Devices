using Microsoft.EntityFrameworkCore;

namespace DeviceApi.Models
{
    public class DevicesContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }

        public DevicesContext(DbContextOptions<DevicesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Brand).HasMaxLength(256);
            });
        }
    }
}
