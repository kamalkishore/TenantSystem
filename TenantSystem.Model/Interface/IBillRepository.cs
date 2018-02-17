using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantSystem.Model.Model;

namespace TenantSystem.Model.Interface
{
    public interface IBillRepository
    {
        long Add(Bill bill);
        void Edit(Bill bill);
        void Delete(Bill bill);
        IEnumerable<Bill> GetAll();
        IEnumerable<Bill> GetAll(Tenant tenant);
        IEnumerable<Bill> GetAll(int tenantId);
        Bill Get(long billId);
    }
}
