using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TenantSystem.Models
{
    public class TenantDBContext : DbContext
    {
        public DbSet<ElectricMeter> ElectricMeter { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Tenant> Tenant { get; set; }
        //public DbSet<TenantMeterReading> TenantMeterReading { get; set; }

        //static TenantDBContext()
        //{
        //    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TenantDBContext>());
        //}
    }
}