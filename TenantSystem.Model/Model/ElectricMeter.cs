using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantSystem.Model.Model
{
    public class ElectricMeter
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int MeterType { get; set; }
        public virtual bool IsOccupied { get; set; }
        public virtual DateTime DateOfMeterInstalled { get; set; }
        public virtual long InitialReading { get; set; }
        public virtual long CurrentMeterReading { get; set; }
        public virtual DateTime DateOfCurrentMeterReading { get; set; }
    }
}
