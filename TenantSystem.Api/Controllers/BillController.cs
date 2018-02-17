using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using TenantSystem.Api.Models;
using TenantSystem.BLL;
using TenantSystem.BLL.ViewModel;
using TenantSystem.Infrastructure;
using TenantSystem.Infrastructure.Repository;
using TenantSystem.Model.Interface;

namespace TenantSystem.Api.Controllers
{
    [EnableQuery()]
    public class BillController : ApiController
    {
        private BillLogic _billLogic;
        private TenantLogic _tenantLogic;
        private ITenantRepository _tenantRepo;
        private IMeterReadingRepository _meterReadingRepo;
        private IPreviousReadingDetailRepository _previousReadingRepo;
        private IElectricMeterRepository _electricMeterRepo;

        public BillController()
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

        // GET: api/Bill
        public Envelope<IQueryable<BillViewModel>> Get()
        {
            return Envelope.Ok(_billLogic.GetAllBills().AsQueryable());
        }

        // GET: api/Bill/5
        public BillViewModel Get(int id)
        {            
            //var tenant = _tenantRepo.Get(id);
            //return _billLogic.GetAllBillsFor(tenant);
            return null;
        }

        //[Route("Bill\Tenant\Id")]
        //public Envelope<IQueryable<BillViewModel>> Get(int tenantId)
        //{
        //    return Envelope.Ok(_billLogic.GetAllBills().AsQueryable());
        //}

        // POST: api/Bill
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Bill/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bill/5
        public void Delete(int id)
        {
        }
    }
}
