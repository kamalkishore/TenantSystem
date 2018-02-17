using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TenantSystem.BLL;
using TenantSystem.Infrastructure;
using TenantSystem.Infrastructure.Repository;
using TenantSystem.Model.Interface;

namespace TenantSystem.Api.Controllers
{
    public class PaymentController : ApiController
    {
        private BillLogic _billLogic;
        private TenantLogic _tenantLogic;
        private ITenantRepository _tenantRepo;
        private IMeterReadingRepository _meterReadingRepo;
        private IPreviousReadingDetailRepository _previousReadingRepo;
        private IElectricMeterRepository _electricMeterRepo;

        public PaymentController()
        {
            var session = SqlServerSessionFactory.CreateSessionFactory().OpenSession();
            _tenantRepo = new TenantRepository(session);
            _meterReadingRepo = new MeterReadingRepository(session);
            _previousReadingRepo = new PreviousReadingDetailRepository(session);
            _billLogic = new BillLogic(new BillRepository(session));
            _electricMeterRepo = new ElectricMeterRepository(session);
            _tenantLogic = new TenantLogic(_billLogic,
                                           _tenantRepo,
                                           _meterReadingRepo,
                                           _electricMeterRepo,
                                           _previousReadingRepo);
        }

        // GET: api/Payment
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Payment/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Payment
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Payment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Payment/5
        public void Delete(int id)
        {
        }
    }
}
