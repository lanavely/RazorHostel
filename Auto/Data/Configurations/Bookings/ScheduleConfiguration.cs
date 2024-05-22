using Auto.Data.Entities.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auto.Data.Configurations.Bookings;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(s => s.ScheduleId);

        builder.HasOne(s => s.School)
            .WithOne(s => s.Schedule)
            .HasForeignKey<Schedule>(s => s.SchoolId)
            .IsRequired();
    }
}