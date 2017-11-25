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
    public class PreviousReadingDetailRepository : IPreviousReadingDetailRepository
    {
        private ISession _session;
        public PreviousReadingDetailRepository(ISession iSession)
        {
            _session = iSession;
        }

        public int Add(TenantPreviousReadingDetails previousReading)
        {
            int id;
            using (ITransaction transaction = _session.BeginTransaction())
            {
                id = (int)_session.Save(previousReading);
                transaction.Commit();
            }

            return id;
        }

        public void Delete(TenantPreviousReadingDetails previousReading)
        {
            throw new NotImplementedException();
        }

        public TenantPreviousReadingDetails Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TenantPreviousReadingDetails> GetAll()
        {
            return _session.QueryOver<TenantPreviousReadingDetails>().List();
        }

        public TenantPreviousReadingDetails Get(Tenant tenant)
        {
            return _session.QueryOver<TenantPreviousReadingDetails>()
                .Where(x => x.Tenant.Id == tenant.Id)
                .List().FirstOrDefault();
        }

        public void Update(TenantPreviousReadingDetails previousReading)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(previousReading);
                transaction.Commit();
            }
        }
    }
}
