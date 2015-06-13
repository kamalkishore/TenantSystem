using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TenantSystem.Models
{
    public class TenantMeterReading
    {
        public long Id { get; set; }
        [Required]
        public int TenantId { get; set; }
        [Required]
        [DisplayName("Meter Id")]
        public int MeterId { get; set; }
        [Required]
        [DisplayName("Per Unit Price")]
        public decimal PerUnitPrice { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [DisplayName("Current Meter Reading")]
        public long CurrentMeterReading { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Previous Meter Reading")]
        public long PreviousMeterReading { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Previous Month Reading Date")]
        public DateTime DateOfPreviousMonthMeterReading { get; set; }
        [DisplayName("Amount Payable")]
        public double AmountPayable { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date of Current Month Reading")]
        public DateTime DateOfMeterReading { get; set; }
        public bool DoesBillGenerated { get; set; }
        public long PaymentId { get; set; }
        
    }
}