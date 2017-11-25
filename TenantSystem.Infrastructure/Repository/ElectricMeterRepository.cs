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
    public class ElectricMeterRepository : IElectricMeterRepository
    {
        private ISession _session;
        public ElectricMeterRepository(ISession iSession)
        {
            _session = iSession;
        }

        public int Add(ElectricMeter meter)
        {
            int id;
            using (ITransaction transaction = _session.BeginTransaction())
            {
                id = (int)_session.Save(meter);
                transaction.Commit();
            }

            return id;
        }

        public void Delete(ElectricMeter meter)
        {
            throw new NotImplementedException();
        }

        public void Update(ElectricMeter meter)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(meter);
                transaction.Commit();
            }
        }

        public IEnumerable<ElectricMeter> GetAll()
        {
            return _session.QueryOver<ElectricMeter>().List();
        }

        public ElectricMeter Get(int id)
        {
            return _session.Get<ElectricMeter>(id);
        }        
    }
}
