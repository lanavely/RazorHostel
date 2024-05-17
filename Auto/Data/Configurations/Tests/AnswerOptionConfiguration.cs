using Auto.Data.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations.Tests;

public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
{
    public void Configure(EntityTypeBuilder<AnswerOption> builder)
    {
        builder.HasKey(o => o.AnswerId);

        builder.HasOne(o => o.Question)
            .WithMany(q => q.AnswerOptions)
            .HasForeignKey(o => o.QuestionId)
            .IsRequired();
    }
}