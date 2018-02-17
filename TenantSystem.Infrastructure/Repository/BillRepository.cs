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
    public class BillRepository : IBillRepository
    {
        private ISession _session;
        public BillRepository(ISession iSession)
        {
            _session = iSession;
        }

        public long Add(Bill bill)
        {
            long billId;
            using (ITransaction transaction = _session.BeginTransaction())
            {
                billId = (long)_session.Save(bill);
                transaction.Commit();
            }

            return billId;
        }

        public void Delete(Bill bill)
        {
            throw new NotImplementedException();
        }

        public void Edit(Bill bill)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(bill);
                transaction.Commit();
            }
        }

        public IEnumerable<Bill> GetAll()
        {
            return _session.QueryOver<Bill>().List();
        }

        public IEnumerable<Bill> GetAll(Tenant tenant)
        {
            return _session.QueryOver<Bill>()
                .Where(x => x.Tenant.Id == tenant.Id)
                .List();
        }

        public IEnumerable<Bill> GetAll(int tenantId)
        {
            return _session.QueryOver<Bill>()
                .Where(x => x.Tenant.Id == tenantId)
                .List();
        }

        public Bill Get(long billId)
        {
            return _session.Get<Bill>(billId);
        }
    }
}
