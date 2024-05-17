using Auto.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasMany(e => e.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }
}