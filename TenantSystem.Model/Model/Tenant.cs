using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantSystem.Model.Model
{
    public class Tenant
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string NickName { get; set; }
        public virtual int PhoneNumber { get; set; }
        public virtual string FullName { get; set; }
        //public virtual ElectricMeter ElectricMeter { get; set; }
        //public Building Building { get; set; }
        //public Floor Floor { get; set; }
        //public virtual BalanceDetails BalanceDetails { get; set; }
        //public virtual Bill LastBill { get; set; }
    }
}
