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
        public DbSet<Room> Rooms { get; set; }

        public DbSet<Client> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Hostel.DataAccess.Entities.Hostel> Hostels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new HostelConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

