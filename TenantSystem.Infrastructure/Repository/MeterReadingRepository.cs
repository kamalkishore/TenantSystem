using NHibernate;
using System;
using System.Collections.Generic;
using TenantSystem.Model.Interface;
using TenantSystem.Model.Model;

namespace TenantSystem.Infrastructure.Repository
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private ISession _session;
        public MeterReadingRepository(ISession iSession)
        {
            _session = iSession;
        }

        public long Add(TenantMeterReading reading)
        {
            long id;
            using (ITransaction transaction = _session.BeginTransaction())
            {
                id = (long)_session.Save(reading);
                transaction.Commit();
            }

            return id;
        }

        public void Delete(TenantMeterReading reading)
        {
            throw new NotImplementedException();
        }

        public void Edit(TenantMeterReading reading)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(reading);
                transaction.Commit();
            }
        }

        public IEnumerable<TenantMeterReading> GetAll()
        {
            return _session.QueryOver<TenantMeterReading>().List();
        }

        public IEnumerable<TenantMeterReading> GetAll(Tenant tenant)
        {
            return _session.QueryOver<TenantMeterReading>()
                .Where(x => x.Tenant.Id == tenant.Id)
                .List();
        }

        public TenantMeterReading Get(long id)
        {
            return _session.Get<TenantMeterReading>(id);
        }
    }
}
