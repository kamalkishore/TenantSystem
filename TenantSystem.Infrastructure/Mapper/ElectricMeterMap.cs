using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantSystem.Model.Model;

namespace TenantSystem.Infrastructure.Mapper
{
    public class ElectricMeterMap : ClassMap<ElectricMeter>
    {
        public ElectricMeterMap()
        {
            Table("ElectricMeters");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("Id");
            Map(x => x.Name).Column("Name").Not.Nullable();
            Map(x => x.MeterType).Column("MeterType").Not.Nullable();
            Map(x => x.IsOccupied).Column("IsOccupied").Not.Nullable();
            Map(x => x.DateOfMeterInstalled).Column("DateOfMeterInstalled").Not.Nullable();
            Map(x => x.InitialReading).Column("InitialReading").Not.Nullable();
            Map(x => x.CurrentMeterReading).Column("CurrentMeterReading").Not.Nullable();
            Map(x => x.DateOfCurrentMeterReading).Column("DateOfCurrentMeterReading").Not.Nullable();
        }
    }
}
