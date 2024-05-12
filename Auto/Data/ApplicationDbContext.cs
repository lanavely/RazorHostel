using Auto.Data.Configurations;
using Auto.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auto.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<SchoolEntity> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

