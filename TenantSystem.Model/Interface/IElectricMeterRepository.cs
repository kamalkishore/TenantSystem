using System.Collections.Generic;
using TenantSystem.Model.Model;

namespace TenantSystem.Model.Interface
{
    public interface IElectricMeterRepository
    {
        int Add(ElectricMeter meter);
        void Update(ElectricMeter meter);
        void Delete(ElectricMeter meter);
        IEnumerable<ElectricMeter> GetAll();
        ElectricMeter Get(int id);
    }
}
