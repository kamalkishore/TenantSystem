using System;

namespace TenantSystem.Model.Model
{
    public class TenantPayment
    {
        public virtual long Id { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual float Amount { get; set; }
        public virtual DateTime DateOfPayment { get; set; }
        public virtual string Comments { get; set; }
        public virtual int PaymentType { get; set; }
    }
}
