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
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DisplayName("Meter Type")]
        public ElecticityMeterType MeterType { get; set; }
        public bool IsOccupied { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Of Meter Installed")]
        public DateTime DateOfMeterInstalled { get; set; }
        [DisplayName("Initial Reading")]
        public long InitialReading { get; set; }        
        [DisplayName("Current Meter Reading")]
        public long CurrentMeterReading { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Current Reading")]
        public DateTime DateOfCurrentMeterReading { get; set; }
    }

    public enum ElecticityMeterType
    {
        SubMeter = 0,
        MainMeter = 1
    }
}