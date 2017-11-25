using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantSystem.Model.Model;

namespace TenantSystem.Model.Interface
{
    public interface IMeterReadingRepository
    {
        long Add(TenantMeterReading reading);
        void Edit(TenantMeterReading reading);
        void Delete(TenantMeterReading reading);
        IEnumerable<TenantMeterReading> GetAll();
        IEnumerable<TenantMeterReading> GetAll(Tenant tenant);
        TenantMeterReading Get(long id);
    }
}
