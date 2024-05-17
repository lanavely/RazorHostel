using Auto.Data.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations.Tests;

public class QuestionCategoryConfiguration : IEntityTypeConfiguration<QuestionCategory>
{
    public void Configure(EntityTypeBuilder<QuestionCategory> builder)
    {
        builder.HasKey(c => c.CategoryId);
    }
}