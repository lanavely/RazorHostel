using Auto.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations
{
    public class SchoolConfiguration : IEntityTypeConfiguration<SchoolEntity>
    {
        public void Configure(EntityTypeBuilder<SchoolEntity> builder)
        {
            builder.HasKey(x => x.IdSchool);

            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Description);
            builder.Property(x => x.Address).HasMaxLength(200);
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(100);
        }
    }
}
