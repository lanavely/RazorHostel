using Hostel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostel.DataAccess.Configurations
{
    public class HostelConfiguration : IEntityTypeConfiguration<HostelEntity>
    {
        public void Configure(EntityTypeBuilder<HostelEntity> builder)
        {
            builder.HasKey(x => x.IdHostel);

            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Description);
            builder.Property(x => x.Address).HasMaxLength(200);
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(100);

            builder.HasMany(x => x.Rooms)
                .WithOne(x => x.Hostel)
                .HasPrincipalKey(x => x.IdHostel)
                .HasForeignKey(x => x.IdHostel)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
