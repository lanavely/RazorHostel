using Auto.Data.Entities;
using Auto.Data.Entities.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations.Bookings;

public class ScheduleItemConfiguration : IEntityTypeConfiguration<ScheduleItem>
{
    public void Configure(EntityTypeBuilder<ScheduleItem> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Schedule)
            .WithMany(s => s.ScheduleItems)
            .HasForeignKey(s => s.ScheduleId)
            .IsRequired();
    }
}