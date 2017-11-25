using FluentNHibernate.Mapping;
using TenantSystem.Model.Model;

namespace TenantSystem.Infrastructure.Mapper
{
    public class TenantMeterReadingMap : ClassMap<TenantMeterReading>
    {
        public TenantMeterReadingMap()
        {
            Table("TenantMeterReadings");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("Id");
            References(x => x.Tenant).Column("TenantId");
            Map(x => x.MeterId).Column("MeterId").Not.Nullable();
            Map(x => x.PerUnitPrice).Column("PerUnitPrice").Not.Nullable();
            Map(x => x.CurrentMeterReading).Column("CurrentMeterReading").Not.Nullable();
            Map(x => x.PreviousMeterReading).Column("PreviousMeterReading").Not.Nullable();
            Map(x => x.DateOfPreviousMonthMeterReading).Column("DateOfPreviousMonthMeterReading").Not.Nullable();
            Map(x => x.AmountPayable).Column("AmountPayable").Not.Nullable();
            Map(x => x.DateOfMeterReading).Column("DateOfMeterReading").Not.Nullable();
            Map(x => x.DoesBillGenerated).Column("DoesBillGenerated").Not.Nullable();
            Map(x => x.PaymentId).Column("PaymentId").Not.Nullable();
        }
    }
}
