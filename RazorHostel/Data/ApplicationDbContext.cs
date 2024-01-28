using Hostel.DataAccess.Configurations;
using Hostel.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RazorHostel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RoomEntity> Rooms { get; set; }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<BookingEntity> Bookings { get; set; }

        public DbSet<HostelEntity> Hostels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new HostelConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

