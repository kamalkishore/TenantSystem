using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantSystem.BLL.ViewModel
{
    public class MeterReadingViewModel
    {
        public virtual long Id { get; set; }
        public virtual TenantViewModel Tenant { get; set; }
        public virtual int MeterId { get; set; }
        public virtual decimal PerUnitPrice { get; set; }
        public virtual long CurrentMeterReading { get; set; }
        public virtual long PreviousMeterReading { get; set; }
        public virtual DateTime DateOfPreviousMonthMeterReading { get; set; }
        public virtual double AmountPayable { get; set; }
        public virtual DateTime DateOfMeterReading { get; set; }
        public virtual bool DoesBillGenerated { get; set; }
        public virtual long PaymentId { get; set; }
    }
}
