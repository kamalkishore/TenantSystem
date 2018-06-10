using FluentNHibernate.Mapping;
using TenantSystem.Model.Model;

namespace TenantSystem.Infrastructure.Mapper
{
    public class TenantPaymentMap : ClassMap<TenantPayment>
    {
        public TenantPaymentMap()
        {
            Table("TenantPayments");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("Id");
            References(x => x.Tenant).Column("TenantId");
            Map(x => x.Amount).Column("Amount").Not.Nullable();
            Map(x => x.DateOfPayment).Column("DateOfPayment").Not.Nullable();
            Map(x => x.Comments).Column("Comments");
            Map(x => x.PaymentType).Column("PaymentType").Not.Nullable();
        }
    }
}
