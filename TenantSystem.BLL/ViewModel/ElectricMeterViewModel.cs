using System;

namespace TenantSystem.BLL.ViewModel
{
    public class ElectricMeterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MeterType { get; set; }
        public bool IsOccupied { get; set; }
        public DateTime DateOfMeterInstalled { get; set; }
        public long InitialReading { get; set; }
        public long CurrentMeterReading { get; set; }
        public DateTime DateOfCurrentMeterReading { get; set; }
    }
}
