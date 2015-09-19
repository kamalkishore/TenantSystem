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

        }
    }
}
