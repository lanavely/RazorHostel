using Auto.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.BookingId);

            builder.Property(x => x.Comment).HasMaxLength(1000);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasPrincipalKey(k => k.Id)
                .HasForeignKey(k => k.UserId)
                .IsRequired();
            
            builder.HasOne(b => b.School)
                .WithMany(u => u.Bookings)
                .HasPrincipalKey(k => k.IdSchool)
                .HasForeignKey(k => k.IdSchool)
                .IsRequired();
        }
    }
}
