using Auto.Data.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations.Tests;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.QuestionId);

        builder.HasOne(q => q.Category)
            .WithMany(c => c.Questions)
            .HasForeignKey(q => q.CategoryId)
            .IsRequired();
        
        builder.HasOne(q => q.Image)
            .WithOne(i => i.Question)
            .HasForeignKey<Question>(q => q.ImageId)
            .IsRequired();
    }
}