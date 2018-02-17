using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TenantSystem.Api.Models;
using TenantSystem.BLL;
using TenantSystem.BLL.ViewModel;
using TenantSystem.Infrastructure;
using TenantSystem.Infrastructure.Repository;
using TenantSystem.Model.Interface;

namespace TenantSystem.Api.Controllers
{
    public class TenantController : BaseController
    {
        private BillLogic _billLogic;
        private TenantLogic _tenantLogic;
        private ITenantRepository _tenantRepo;
        private IMeterReadingRepository _meterReadingRepo;
        private IPreviousReadingDetailRepository _previousReadingRepo;
        private IElectricMeterRepository _electricMeterRepo;

        public TenantController()
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

        // GET: api/Tenant
        public IHttpActionResult Get()
        {
            return Ok(_tenantLogic.GetTenants());
        }

        [Route("api/tenant/{id}/bills")]
        public IHttpActionResult GetBills(int id)
        {
            try
            {
                var result = _billLogic.GetAllBillsFor(id);

                if (result.IsFailure)
                    return Error(result.Error);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.BadGateway);
            }            
        }

        // GET: api/Tenant/5
        public Envelope<TenantViewModel> Get(int id)
        {
            return Envelope.Ok(_tenantLogic.GetTenant(id));
        }

        // POST: api/Tenant
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Tenant/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tenant/5
        public void Delete(int id)
        {
        }
    }
}
