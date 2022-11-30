using Microsoft.EntityFrameworkCore;
using System;
using App.Models.Contacts;
using App.Models.Payments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using App.Models.Products;

namespace App.Models
{
    public class GymAppDbContext : IdentityDbContext<AppUser>

    {
        public GymAppDbContext(DbContextOptions<GymAppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Slug).IsUnique();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(p => p.Slug).IsUnique();
            });

            modelBuilder.Entity<PaymentDetail>(entity =>
            {
                entity.HasKey(pd => new { pd.PaymentID, pd.ProductID });
            });
        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPhoto> ProductPhotos { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentDetail> PaymentDetails { get; set; }
    }
}