using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TenantSystem.Models
{
    public class TenantPreviousReadingDetails
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
        [DataType(DataType.Currency)]
        [DisplayName("Previous Meter Reading")]
        public long PreviousMeterReading { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Previous Month Reading Date")]
        public DateTime DateOfPreviousMonthMeterReading { get; set; }
        [DisplayName("Amount Payable")]
        public double AmountPayable { get; set; }
    }
}
