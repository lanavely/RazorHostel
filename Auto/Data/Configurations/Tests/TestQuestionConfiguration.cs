using Auto.Data.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations.Tests;

public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
{
    public void Configure(EntityTypeBuilder<TestQuestion> builder)
    {
        builder.HasKey(b => b.IdTestQuestion);

        builder.HasOne(t => t.Question)
            .WithMany()
            .HasForeignKey(t => t.IdQuestion)
            .IsRequired();

        builder.HasOne(t => t.Answer)
            .WithMany()
            .HasForeignKey(tq => tq.IdSelectedAnswer);

        builder.HasOne(t => t.Test)
            .WithMany(t => t.Questions)
            .HasForeignKey(tq => tq.IdTest);
    }
}