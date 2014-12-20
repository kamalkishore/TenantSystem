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
        [DataType(DataType.Text)]
        public long MeterReading { get; set; }
        public decimal AmountPayable { set { value = (this.MeterReading * this.PerUnitPrice); } }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfMeterReading { get; set; }
        
    }
}