using Microsoft.EntityFrameworkCore;

namespace Test.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Chronic_disease>().ToTable("chronic_diseases");
            modelBuilder.Entity<UserChronicDisease>().ToTable("user_chronic_diseases").HasKey("user_id", "disease_id");
            modelBuilder.Entity<Email_OTP>().ToTable("email_otp").HasKey("Email");
            modelBuilder.Entity<Drug>().ToTable("drugs");
            modelBuilder.Entity<Symptom>().ToTable("symptoms");
            modelBuilder.Entity<Post>().ToTable("posts");

        }
    }
}
