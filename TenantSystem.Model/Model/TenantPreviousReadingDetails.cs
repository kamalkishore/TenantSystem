using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantSystem.Model.Model
{
    public class TenantPreviousReadingDetails
    {
        public virtual long Id { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual int MeterId { get; set; }
        public virtual decimal PerUnitPrice { get; set; }
        public virtual long PreviousMeterReading { get; set; }
        public virtual DateTime DateOfPreviousMonthMeterReading { get; set; }
        public virtual double AmountPayable { get; set; }
    }
}
