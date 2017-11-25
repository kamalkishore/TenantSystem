using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantSystem.BLL.ViewModel
{
    public class BillViewModel
    {
        public long Id { get; set; }
        public string TenantName { get; set; }
        public long PreviousMonthReading { get; set; }
        public DateTime PreviousMonthReadingDate { get; set; }
        public long CurrentMonthReading { get; set; }
        public DateTime CurrentMonthReadingDate { get; set; }
        public double PerUnitPrice { get; set; }
        public long UnitConsumed { get; set; }
        public double CurrentMonthPayableAmount { get; set; }
        public double PreviousMonthPendingAmount { get; set; }
        public double LastPaidAmount { get; set; }
        public DateTime LastPaidAmountDate { get; set; }
        public double TotalPayableAmount { get; set; }
        public int Year { get; set; }
    }
}
