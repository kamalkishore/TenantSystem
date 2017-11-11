using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantSystem.Model.Model
{
    public class Bill
    {
        public virtual Tenant Tenant { get; set; }
        public virtual int PreviousReading { get; set; }
        public virtual DateTime PreviousReadingDate { get; set; }
        public virtual int CurrentReading { get; set; }
        public virtual DateTime CurrentReadingDate { get; set; }
        public virtual double PricePerUnit { get; set; }
        public virtual int UnitConsumed { get; set; }
        public virtual double CurrentPayableAmount { get; set; }
        public virtual double PreviousPendingAmount { get; set; }
        public virtual double LastPaidAmount { get; set; }
        public virtual DateTime LastPaidAmountDate { get; set; }
        public virtual double TotalAmount { get; set; }

        public virtual long Id { get; set; }
        //public int TenantId { get; set; }
        //public string TenantName { get; set; }

        //public virtual DateTime PreviousMonthReadingDate { get; set; }
        //public virtual long PreviousMonthReading { get; set; }
        //public virtual DateTime CurrentMonthReadingDate { get; set; }
        //public virtual long CurrentMonthReading { get; set; }
        //public virtual long UnitConsumed { get; set; }
        //public virtual double LastPaidAmount { get; set; }
        //public virtual DateTime LastPaidAmountDate { get; set; }
        //public virtual double CurrentMonthPayableAmount { get; set; }
        //public virtual double PreviousMonthPendingAmount { get; set; }
        //public virtual double TotalPayableAmount { get; set; }
        //public virtual decimal PerUnitPrice { get; set; }
        //public virtual string MonthName { get; set; } //todo find a better way to retrieve and display month name
    }
}
