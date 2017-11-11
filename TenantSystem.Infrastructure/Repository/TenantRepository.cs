using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantSystem.Model.Interface;
using TenantSystem.Model.Model;

namespace TenantSystem.Infrastructure.Repository
{
    public class TenantRepository : ITenantRepository
    {
        private ISession _session;

        public TenantRepository(ISession session)
        {
            _session = session;
        }

        public int Add(Tenant tenant)
        {
            throw new NotImplementedException();
        }

        public void Delete(Tenant tenant)
        {
            throw new NotImplementedException();
        }

        public Tenant Get(int tenantId)
        {
            return _session.Get<Tenant>(tenantId);
        }

        public IEnumerable<Tenant> GetAll()
        {
            return _session.QueryOver<Tenant>().List();
        }

        public void Update(Tenant tenant)
        {
            throw new NotImplementedException();
        }
    }
}
