using Microsoft.EntityFrameworkCore;
using skillz_backend.models;
using System.Security.Cryptography;

namespace skillz_backend.data
{
    public class SkillzDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobImage> JobImages { get; set; }
        public DbSet<ReviewJob> ReviewsJob { get; set; }
        public DbSet<ReviewUser> ReviewsUser { get; set; }
        public DbSet<CertificatUser> CertificatsUser { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        public SkillzDbContext(DbContextOptions<SkillzDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurarea relației dintre User și tabelul Users
            modelBuilder.Entity<User>().ToTable("Users");
            // Configurarea relației dintre Job și User
            modelBuilder.Entity<Job>()
                .HasOne(j => j.User)
                .WithMany(u => u.Jobs)
                .HasForeignKey(j => j.IdUser);
            // Configure relationship between Job and JobImage
            modelBuilder.Entity<JobImage>()
                .HasOne(ji => ji.Job)
                .WithMany(j => j.Images)
                .HasForeignKey(ji => ji.JobId);
            // Configurarea relației dintre Review și Job
            modelBuilder.Entity<ReviewJob>()
                .HasOne(r => r.Job)
                .WithMany(j => j.ReviewsJob)
                .HasForeignKey(r => r.IdJob);
            // Configurarea relatiei dintre Review si User
            modelBuilder.Entity<ReviewUser>()
                .HasOne(r => r.User)
                .WithMany(j => j.ReviewsUser)
                .HasForeignKey(r => r.IdUser);
            // Configurarea relației dintre Certificate și User
            modelBuilder.Entity<CertificatUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.UserCertificates)
                .HasForeignKey(cu => cu.IdUser);

            modelBuilder.Entity<CertificatUser>()
                .HasOne(cu => cu.User)
                .WithMany(c => c.UserCertificates)
                .HasForeignKey(cu => cu.IdUser);
            // Configurarea relației dintre Badge și User
            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBadges)
                .HasForeignKey(ub => ub.IdUser);

            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.Badge)
                .WithMany(b => b.UserBadges)
                .HasForeignKey(ub => ub.IdBadge);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ProviderUser)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.ProviderUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
               .HasOne(b => b.Job)
               .WithMany(j => j.Bookings)
               .HasForeignKey(b => b.ProviderUserId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
