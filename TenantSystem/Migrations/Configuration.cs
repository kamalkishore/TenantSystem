namespace TenantSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TenantSystem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TenantSystem.Models.TenantDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TenantSystem.Models.TenantDBContext context)
        {
            context.Building.AddOrUpdate(b => b.Name,
                new Building { Name = "Hall", NumberOfFloors = 4 },
                new Building { Name = "Home", NumberOfFloors = 4 }
                );

            context.ElectricMeter.AddOrUpdate(em => em.Id,
                new ElectricMeter { Id = 1, Name = "Hall First Floor Meter 1", MeterType = ElecticityMeterType.SubMeter },
                new ElectricMeter { Id = 2, Name = "Hall Second Floor Meter 1", MeterType = ElecticityMeterType.SubMeter },
                new ElectricMeter { Id = 3, Name = "Hall Third Floor Meter 1", MeterType = ElecticityMeterType.SubMeter },
                new ElectricMeter { Id = 4, Name = "Hall Fourth Floor Meter 1", MeterType = ElecticityMeterType.SubMeter },
                new ElectricMeter { Id = 5, Name = "Home First Room Meter 1", MeterType = ElecticityMeterType.SubMeter },
                new ElectricMeter { Id = 6, Name = "Home Second Room Meter 1", MeterType = ElecticityMeterType.SubMeter },
                new ElectricMeter { Id = 7, Name = "Home Third Room Meter 1", MeterType = ElecticityMeterType.SubMeter },
                new ElectricMeter { Id = 8, Name = "Home Fourth Room Meter 1", MeterType = ElecticityMeterType.SubMeter },
                new ElectricMeter { Id = 9, Name = "Hall Second Floor Meter 2", MeterType = ElecticityMeterType.SubMeter },
                new ElectricMeter { Id = 10, Name = "Hall Third Floor Meter 2", MeterType = ElecticityMeterType.SubMeter }
                );

            //context.Tenant.AddOrUpdate(t => t.FirstName,
            //    new Tenant
            //    {
            //        FirstName = "Satinder",
            //        LastName = "Kumar",
            //        PhoneNumber = "1234567890",
            //        FullName = "Satinder Kumar",
            //        Meter = context.ElectricMeter.Where(e => e.Id.Equals(1)).FirstOrDefault()
            //    },
            //new Tenant
            //{
            //    FirstName = "Daljeet",
            //    LastName = "Singh",
            //    PhoneNumber = "1234567890",
            //    FullName = "Daljeet Singh",
            //    Meter = context.ElectricMeter.Where(e => e.Id.Equals(2)).FirstOrDefault()
            //},
            //new Tenant
            //{
            //    FirstName = "Rakesh",
            //    LastName = "Kumar",
            //    PhoneNumber = "1234567890",
            //    FullName = "Rakesh Kumar",
            //    Meter = context.ElectricMeter.Where(e => e.Id.Equals(3)).FirstOrDefault()
            //},
            //new Tenant
            //{
            //    FirstName = "Bhushan",
            //    LastName = "Kumar",
            //    PhoneNumber = "1234567890",
            //    FullName = "Bhushan Kumar",
            //    Meter = context.ElectricMeter.Where(e => e.Id.Equals(4)).FirstOrDefault()
            //},
            //new Tenant
            //{
            //    FirstName = "Room 1",
            //    LastName = "(Bahar wala)",
            //    PhoneNumber = "1234567890",
            //    FullName = "Room 1 (Bahar wala)",
            //    Meter = context.ElectricMeter.Where(e => e.Id.Equals(5)).FirstOrDefault()
            //},
            //new Tenant
            //{
            //    FirstName = "Room 2",
            //    LastName = "(Rasoi wala)",
            //    PhoneNumber = "1234567890",
            //    FullName = "Room (Rasoi wala)",
            //    Meter = context.ElectricMeter.Where(e => e.Id.Equals(6)).FirstOrDefault()
            //},
            //new Tenant
            //{
            //    FirstName = "Room 3",
            //    LastName = "????",
            //    PhoneNumber = "1234567890",
            //    FullName = "Room 3 ????",
            //    Meter = context.ElectricMeter.Where(e => e.Id.Equals(7)).FirstOrDefault()
            //},
            //new Tenant
            //{
            //    FirstName = "Room 4",
            //    LastName = "(Naaj wala)",
            //    PhoneNumber = "1234567890",
            //    FullName = "Room 4 (Naaj wala)",
            //    Meter = context.ElectricMeter.Where(e => e.Id.Equals(8)).FirstOrDefault()
            //}
            //);

              //This method will be called after migrating to the latest version.

              //You can use the DbSet<T>.AddOrUpdate() helper extension method 
              //to avoid creating duplicate seed data. E.g.
            
              //  context.People.AddOrUpdate(
              //    p => p.FullName,
              //    new Person { FullName = "Andrew Peters" },
              //    new Person { FullName = "Brice Lambson" },
              //    new Person { FullName = "Rowan Miller" }
              //  );
            
        }
    }
}
