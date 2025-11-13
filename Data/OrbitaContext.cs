using Microsoft.EntityFrameworkCore;
using Orbita.CareerApi.Models;

namespace Orbita.CareerApi.Data
{
    public class OrbitaContext : DbContext
    {
        public OrbitaContext(DbContextOptions<OrbitaContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<CareerPath> CareerPaths => Set<CareerPath>();
        public DbSet<Mission> Missions => Set<Mission>();
        public DbSet<UserMissionProgress> UserMissionProgresses => Set<UserMissionProgress>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CareerPath>()
                .HasMany(c => c.Missions)
                .WithOne(m => m.CareerPath)
                .HasForeignKey(m => m.CareerPathId);

            modelBuilder.Entity<UserMissionProgress>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<UserMissionProgress>()
                .HasOne(p => p.Mission)
                .WithMany()
                .HasForeignKey(p => p.MissionId);
        }
    }
}
