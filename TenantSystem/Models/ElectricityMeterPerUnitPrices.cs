using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TenantSystem.Models
{
    public class ElectricityMeterPerUnitPrices
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Unit Price")]
        public double Value { get; set; }
    }
}
