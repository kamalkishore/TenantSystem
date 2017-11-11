using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantSystem.Model.Model;

namespace TenantSystem.Infrastructure.Mapper
{
    public class TenantMap : ClassMap<Tenant>
    {
        public TenantMap()
        {
            Table("Tenants");
            //LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("Id");
            //References(x => x.ElectricMeter).Column("ElectricMeterId");
            //References(x => x.LastBill).Column("LastBillId");
            //References(x => x.BalanceDetails).Column("BalanceDetailsId");
            Map(x => x.FirstName).Column("FirstName").Not.Nullable();
            Map(x => x.MiddleName).Column("MiddleName");
            Map(x => x.LastName).Column("LastName").Not.Nullable();
            Map(x => x.NickName).Column("NickName");
            Map(x => x.PhoneNumber).Column("PhoneNumber").Not.Nullable();
            //Map(x => x.ElectricMeterId).Column("ElectricMeterId").Not.Nullable();
            //Map(x => x.FullName).Column("FullName");
        }
    }
}
