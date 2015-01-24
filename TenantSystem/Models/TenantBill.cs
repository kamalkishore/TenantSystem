using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TenantSystem.Models
{
    public class TenantBill
    {
        public long Id { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        [DisplayName("Previous Month Reading")]
        public long PreviousMonthReading { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime CurrentMonthReadingDate { get; set; }
        [DisplayName("Current Month Reading")]
        public long CurrentMonthReading { get; set; }
        [DisplayName("Last Amount Paid")]
        public double LastPaidAmount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime LastPaidAmountDate { get; set; }
        [DisplayName("Amount Payable for the Month")]
        public double CurrentMonthPayableAmount { get; set; }
        [DisplayName("Previous Month Pending Amount")]
        public double PreviousMonthPendingAmount { get; set; }
        [DisplayName("Total Amount Payable")]
        public double TotalPayableAmount { get; set; }
        public decimal PerUnitPrice { get; set; }
    }
}