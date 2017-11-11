using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantSystem.Model.Model;

namespace TenantSystem.Infrastructure.Mapper
{
    class BillMap : ClassMap<Bill>
    {
        public BillMap()
        {
            Table("TenantBills");
            //LazyLoad();
            References(x => x.Tenant).Column("TenantId");
            Id(x => x.Id).GeneratedBy.Identity().Column("Id");
            Map(x => x.PreviousReadingDate).Column("PreviousMonthReadingDate").Not.Nullable();
            Map(x => x.PreviousReading).Column("PreviousMonthReading").Not.Nullable();
            Map(x => x.CurrentReadingDate).Column("CurrentMonthReadingDate").Not.Nullable();
            Map(x => x.CurrentReading).Column("CurrentMonthReading").Not.Nullable();
            Map(x => x.UnitConsumed).Column("UnitConsumed").Not.Nullable();
            Map(x => x.CurrentPayableAmount).Column("CurrentMonthPayableAmount").Not.Nullable();
            Map(x => x.LastPaidAmount).Column("LastPaidAmount").Not.Nullable();
            Map(x => x.LastPaidAmountDate).Column("LastPaidAmountDate").Not.Nullable();
            Map(x => x.PreviousPendingAmount).Column("PreviousMonthPendingAmount").Not.Nullable();
            Map(x => x.TotalAmount).Column("TotalPayableAmount").Not.Nullable();
            Map(x => x.PricePerUnit).Column("PerUnitPrice").Not.Nullable();
        }
    }
}
