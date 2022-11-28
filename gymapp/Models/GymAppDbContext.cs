using Microsoft.EntityFrameworkCore;
using System;
using App.Models.Contacts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}