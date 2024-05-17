using Auto.Data.Configurations;
using Auto.Data.Configurations.Tests;
using Auto.Data.Entities;
using Auto.Data.Entities.Tests;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auto.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        
        public DbSet<Group> Groups { get; set; }
        
        public DbSet<School> Schools { get; set; }
        
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        
        public DbSet<ImageData> ImageData { get; set; }

        public DbSet<Question> Questions { get; set; }
        
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
 
        public DbSet<Test> Tests { get; set; }
        
        public DbSet<TestQuestion> TestQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            
            modelBuilder.ApplyConfiguration(new AnswerOptionConfiguration());
            modelBuilder.ApplyConfiguration(new ImageDataConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new TestQuestionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

