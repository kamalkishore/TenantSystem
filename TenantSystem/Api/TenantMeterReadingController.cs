using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TenantSystem.Models;

namespace TenantSystem.Api
{
    public class TenantMeterReadingController : ApiController
    {
        private TenantDBContext _db = new TenantDBContext();

        // GET api/tenantmeterreading
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/tenantmeterreading/5
        public string Get(int id)
        {
            var tenant = _db.Tenant.Find(id);
            var previousMonthMeterReading = tenant.MeterReading
                                            .OrderByDescending(x=>x.Id)
                                            .Where(x=>x.DoesBillGenerated == true)
                                            .Select(x=> x.CurrentMeterReading)
                                            .FirstOrDefault();

            return previousMonthMeterReading.ToString();
        }

        // POST api/tenantmeterreading
        public void Post([FromBody]string value)
        {
        }

        // PUT api/tenantmeterreading/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/tenantmeterreading/5
        public void Delete(int id)
        {
        }
    }
}
