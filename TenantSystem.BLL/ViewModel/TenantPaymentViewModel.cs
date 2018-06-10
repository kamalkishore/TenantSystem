using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantSystem.BLL.ViewModel
{
    public class TenantPaymentViewModel
    {
        public virtual long Id { get; set; }
        public virtual TenantViewModel Tenant { get; set; }
        public virtual float Amount { get; set; }
        public virtual DateTime DateOfPayment { get; set; }
        public virtual string Comments { get; set; }
        public virtual int PaymentType { get; set; }
    }
}
