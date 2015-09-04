using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TenantSystem.Models
{
    public class TenantDBContext : DbContext
    {
        public TenantDBContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<ElectricMeter> ElectricMeter { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Tenant> Tenant { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure Column
            modelBuilder.Entity<TenantBill>()
                        .Property(p => p.PreviousMonthReadingDate)
                        .HasColumnType("datetime2");

            modelBuilder.Entity<TenantBill>()
                        .Property(p => p.CurrentMonthReadingDate)
                        .HasColumnType("datetime2");

            modelBuilder.Entity<TenantBill>()
                        .Property(p => p.LastPaidAmountDate)
                        .HasColumnType("datetime2");
        }
        //public DbSet<TenantMeterReading> TenantMeterReading { get; set; }

        //static TenantDBContext()
        //{
        //    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TenantDBContext>());
        //}
    }
}