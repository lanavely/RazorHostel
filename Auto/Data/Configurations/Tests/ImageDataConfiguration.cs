using Auto.Data.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations.Tests;

public class ImageDataConfiguration : IEntityTypeConfiguration<ImageData>
{
    public void Configure(EntityTypeBuilder<ImageData> builder)
    {
        builder.HasKey(i => i.ImageId);
    }
}