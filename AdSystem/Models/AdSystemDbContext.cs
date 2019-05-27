using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdSystem.Models
{
    public class AdSystemDbContext : DbContext
    {
        public DbSet<Ad> Ads { get; set; }
        public DbSet<UserAccount> Accounts { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Advertiser> Advertisers { get; set; }

        public AdSystemDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql("Server=" + Program.config.dbConfig.server + ";Database=" + Program.config.dbConfig.database + ";Uid=" + Program.config.dbConfig.username + ";Pwd=" + Program.config.dbConfig.password + ";Pooling=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserAccount>().HasIndex(a => a.apiKey).IsUnique();
            modelBuilder.Entity<UserAccount>().HasIndex(a => a.username).IsUnique();
        }
    }
}
