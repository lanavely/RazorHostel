using Hostel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostel.DataAccess.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity>
    {
        public void Configure(EntityTypeBuilder<RoomEntity> builder)
        {
            builder.HasKey(x => x.IdRoom);

            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Square).HasMaxLength(15);
            builder.Property(x => x.Comment).HasMaxLength(1000);
            builder.Property(x => x.Description).HasMaxLength(1000);

            builder.HasMany(x => x.Bookings)
                .WithOne(x => x.Room)
                .HasPrincipalKey(x => x.IdRoom)
                .HasForeignKey(x => x.IdRoom)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
