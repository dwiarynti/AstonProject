using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities.DataContext
{
    public class AstonContext : DbContext
    {
        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>().HasKey(m => m.ID);
            modelBuilder.Entity<Location>().HasKey(m => m.ID);
            modelBuilder.Entity<AssetLocation>().HasKey(m => m.ID);
            modelBuilder.Entity<MovementRequest>().HasKey(m => m.ID);
            modelBuilder.Entity<MovementRequestDetail>().HasKey(m => m.ID);
            modelBuilder.Entity<Pref>().HasKey(m => m.ID);

            modelBuilder.Entity<AssetLocation>()
                .HasOne(m => m.Asset)
                .WithMany(m => m.AssetLocation)
                .HasForeignKey(m => m.AssetID);

            modelBuilder.Entity<AssetLocation>()
               .HasOne(m => m.Location)
               .WithMany(p => p.AssetLocation)
               .HasForeignKey(m => m.LocationID);

            modelBuilder.Entity<MovementRequestDetail>()
                .HasOne(m => m.MovementRequest)
                .WithMany(m => m.MovementRequestDetail)
                .HasForeignKey(m => m.MovementRequestID);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Asset> Asset { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<AssetLocation> AssetLocation { get; set; }
        public DbSet<MovementRequest> MovementRequest { get; set; }
        public DbSet<MovementRequestDetail> MovementRequestDetail { get; set; }
        public DbSet<Pref> Pref { get; set; }
    }
}
