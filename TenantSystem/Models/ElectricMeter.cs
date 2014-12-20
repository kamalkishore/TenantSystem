using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TenantSystem.Models
{
    public class ElectricMeter
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public ElecticityMeterType MeterType { get; set; }
    }

    public enum ElecticityMeterType
    {
        SubMeter = 0,
        MainMeter = 1
    }
}