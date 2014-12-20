using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TenantSystem.Models;

namespace TenantSystem.Controllers
{
    public class TenantController : Controller
    {
        private TenantDBContext _db;

        public TenantController()
        {
            _db = new TenantDBContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowTenants()
        {
            return View(_db.Tenant.ToList());
        }

        [HttpGet]
        public ActionResult AddTenant()
        {
            var listOfMeters = _db.ElectricMeter.ToList().Select(x => new SelectListItem{
                                                                                          Value = x.Id.ToString(), 
                                                                                          Text= x.Name });
            ViewBag.Meter = listOfMeters;
            return View();
        }

        [HttpPost]
        public ActionResult AddTenant(Tenant tenant)
        {            
            if (ModelState.IsValid)
            {
                _db.Tenant.Add(tenant);
                _db.SaveChanges();
            }

            return AddTenant();
        }

        [HttpGet]
        public ActionResult AddTenantMeterReading()
        {
            ViewBag.Tenant = _db.Tenant.Select(x => new SelectListItem
                                                                    {
                                                                        Value = x.Id.ToString(),
                                                                        Text = x.FirstName
                                                                    });
            ViewBag.PricePerUnit = new SelectList(new[] { 7, 6.5, 5 });
            return View();
        }

        [HttpPost]
        public ActionResult AddTenantMeterReading(TenantMeterReading tenantMeterReading)
        {
            if (ModelState.IsValid)
            {
                var tenant = _db.Tenant.Find(tenantMeterReading.TenantId);
                tenant.MeterReading.Add(tenantMeterReading);
                _db.SaveChanges();
            }

            return AddTenantMeterReading();
        }
    }
}
