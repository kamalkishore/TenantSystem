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

            context.ElectricityMeterPerUnitPrices.AddOrUpdate(p => p.Value,
                new ElectricityMeterPerUnitPrices { Value = 5 },
                new ElectricityMeterPerUnitPrices { Value = 6.5 },
                new ElectricityMeterPerUnitPrices { Value = 7 }
                );

        }
    }
}
