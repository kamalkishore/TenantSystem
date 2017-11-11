using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantSystem.Model.Model;

namespace TenantSystem.Model.Interface
{
    public interface ITenantRepository
    {
        int Add(Tenant tenant);
        void Update(Tenant tenant);
        void Delete(Tenant tenant);
        IEnumerable<Tenant> GetAll();
        Tenant Get(int tenantId);
    }
}
