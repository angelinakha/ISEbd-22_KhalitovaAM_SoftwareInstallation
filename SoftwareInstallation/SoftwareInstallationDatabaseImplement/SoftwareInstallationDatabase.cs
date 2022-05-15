using System;
using SoftwareInstallationDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;


namespace SoftwareInstallationDatabaseImplement
{
    public class SoftwareInstallationDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-JDHLPBTR\SQLEXPRESS;Initial Catalog=SoftwareInstallationDatabase7;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(m => m.ImplementerId).IsRequired(false);
            modelBuilder.Entity<MessageInfo>().Property(m => m.ClientId).IsRequired(false);
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Package> Packages { set; get; }
        public virtual DbSet<PackageComponent> PackageComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Implementer> Implementers { set; get; }
        public virtual DbSet<MessageInfo> MessagesInfo { set; get; }
    }
}
