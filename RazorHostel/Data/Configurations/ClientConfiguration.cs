using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hostel.DataAccess.Entities;

namespace Hostel.DataAccess.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            builder.HasKey(x => x.IdClient);

            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.Patronymic).HasMaxLength(100);
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.Role).HasMaxLength(50);

            builder.HasMany(x => x.Bookings)
                .WithOne(x => x.Client)
                .OnDelete(DeleteBehavior.Cascade)
                .HasPrincipalKey(x => x.IdClient)
                .HasForeignKey(x => x.IdClient)
                .IsRequired();
        }
    }
}
