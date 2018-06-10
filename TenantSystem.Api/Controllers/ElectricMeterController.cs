using System.Linq;
using System.Web.Http;
using TenantSystem.BLL;
using TenantSystem.BLL.ViewModel;
using TenantSystem.Infrastructure;
using TenantSystem.Infrastructure.Repository;

namespace TenantSystem.Api.Controllers
{
    public class ElectricMeterController : BaseController
    {
        private ElectricMeterService _meterService;

        public ElectricMeterController()
        {
            var session = SqlServerSessionFactory.CreateSessionFactory().OpenSession();
            var meterRepo = new ElectricMeterRepository(session);
            _meterService = new ElectricMeterService(meterRepo);
        }

        // GET: api/ElectricMeter
        public IHttpActionResult Get()
        {
            return Ok(_meterService.GetAllMeters().OrderByDescending(x => x.Id));
        }

        // GET: api/ElectricMeter/5
        public IHttpActionResult Get(int id)
        {
            return Ok(_meterService.GetMeter(id));
        }

        // POST: api/ElectricMeter
        public IHttpActionResult Post([FromBody]ElectricMeterViewModel meter)
        {
            if (ModelState.IsValid)
            {                
                _meterService.AddMeter(meter);
            }

            return Ok();
        }

        // PUT: api/ElectricMeter/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ElectricMeter/5
        public void Delete(int id)
        {
        }
    }
}
