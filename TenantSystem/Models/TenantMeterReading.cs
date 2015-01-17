using System;
using System.Collections.Generic;
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
        public int MeterId { get; set; }
        [Required]
        public decimal PerUnitPrice { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public long CurrentMeterReading { get; set; }
        [DataType(DataType.Currency)]
        public long PreviousMeterReading { get; set; }
        public double AmountPayable { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfMeterReading { get; set; }
        public bool DoesBillGenerated { get; set; }
        
    }
}