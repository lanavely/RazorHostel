using Auto.Data.Entities;
using Auto.Data.Entities.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.BookingId);

            builder.HasOne(b => b.Client)
                .WithMany(u => u.ClientBookings)
                .HasForeignKey(k => k.ClientId)
                .IsRequired();
            
            builder.HasOne(b => b.School)
                .WithMany(u => u.Bookings)
                .HasForeignKey(k => k.SchoolId)
                .IsRequired();

            builder.HasOne(b => b.Teacher)
                .WithMany(t => t.TeacherBookings)
                .HasForeignKey(b => b.TeacherId)
                .IsRequired();

            builder.HasOne(b => b.ScheduleItem)
                .WithMany()
                .HasForeignKey(b => b.ScheduleItemId)
                .IsRequired();
        }
    }
}
