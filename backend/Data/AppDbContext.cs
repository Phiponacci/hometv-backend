using backend.Models.Auth;
using backend.Models.Camera;
using backend.Models.News;
using backend.Models.Sports;
using backend.Models.Weather;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<LocalHeadline> LocalHeadlines { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<WeatherRecord> WeatherRecords { get; set; }

        public DbSet<NewsLink> NewsLinks { get; set; }
        public DbSet<APIDefinition> APIDefs { get; set; }
        public DbSet<SportCategory> Sports { get; set; }
        public DbSet<ScoreRecord> Scores { get; set; }

        public DbSet<Sys> WeatherSysRecords { get; set; }
        public DbSet<Main> WeatherMainRecords { get; set; }
        public DbSet<Coord> WeatherCoordRecords { get; set; }
        public DbSet<Wind> WeatherWindRecords { get; set; }
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScoreRecord>()
               .HasMany(dm => dm.scores)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WeatherRecord>()
               .HasMany(dm => dm.weather)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<City>()
               .HasOne(dm => dm.WeatherRecord)
               .WithMany()
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<WeatherRecord>()
                .HasOne(dm => dm.wind)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<WeatherRecord>()
                .HasOne(dm => dm.coord)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WeatherRecord>()
                .HasOne(dm => dm.main)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WeatherRecord>()
                .HasOne(dm => dm.sys)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
