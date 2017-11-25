using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantSystem.Model.Model;

namespace TenantSystem.Infrastructure.Mapper
{
    public class TenantPreviousReadingDetailsMap : ClassMap<TenantPreviousReadingDetails>
    {
        public TenantPreviousReadingDetailsMap()
        {
            Table("TenantPreviousReadingDetails");
            Id(x => x.Id).GeneratedBy.Identity().Column("Id");
            References(x => x.Tenant).Column("TenantId");
            Map(x => x.MeterId).Column("MeterId").Not.Nullable();
            Map(x => x.PerUnitPrice).Column("PerUnitPrice").Not.Nullable();
            Map(x => x.PreviousMeterReading).Column("PreviousMeterReading").Not.Nullable();
            Map(x => x.DateOfPreviousMonthMeterReading).Column("DateOfPreviousMonthMeterReading").Not.Nullable();
            Map(x => x.AmountPayable).Column("AmountPayable").Not.Nullable();
        }
    }
}
