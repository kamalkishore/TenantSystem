using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TenantSystem.Models
{
    public class ElectricMeter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DisplayName("Meter Type")]
        public ElecticityMeterType MeterType { get; set; }
        public bool IsOccupied { get; set; }
    }

    public enum ElecticityMeterType
    {
        SubMeter = 0,
        MainMeter = 1
    }
}