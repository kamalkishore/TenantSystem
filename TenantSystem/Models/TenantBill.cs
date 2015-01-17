using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenantSystem.Models
{
    public class TenantBill
    {
        public long Id { get; set; }
        public int TenantId { get; set; }
        public long PreviousMonthReading { get; set; }
        public long CurrentMonthReading { get; set; }
        public double LastPaidAmount { get; set; }
        public DateTime LastPaidAmountDate { get; set; }
        public double CurrentMonthPayableAmount { get; set; }
        public double PreviousMonthPendingAmount { get; set; }
        public double TotalPayableAmount { get; set; }
    }
}