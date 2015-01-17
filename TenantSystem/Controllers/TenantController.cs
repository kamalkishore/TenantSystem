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
                                                                        Text = x.FullName
                                                                    });
            ViewBag.PreviousMeterReading = _db.Tenant.Select(x => new SelectListItem
                                                                        {
                                                                            Value = x.MeterReading
                                                                                     .OrderByDescending(y => y.Id)
                                                                                     .Where(z => z.DoesBillGenerated == true)
                                                                                     .Select(i => i.CurrentMeterReading)
                                                                                     .FirstOrDefault()
                                                                                     .ToString(),
                                                                            Text = x.Id.ToString()
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

        [HttpGet]
        public ActionResult GetPreviousMeterReading(int Id)
        {
            var tenant = _db.Tenant.Find(Id);
            var prevReading = tenant.MeterReading
                                    .OrderByDescending(x => x.Id)
                                    //.Where(y => y.DoesBillGenerated)
                                    .Select(z => z.CurrentMeterReading)
                                    .FirstOrDefault();
            return Json(new { MeterReading = prevReading },JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddTenantPayment()
        {
            ViewBag.Tenant = _db.Tenant.Select(x => new SelectListItem
                                                {
                                                    Value = x.Id.ToString(),
                                                    Text = x.FirstName
                                                });

            return View();
        }

        [HttpPost]
        public ActionResult AddTenantPayment(TenantPayment payment)
        {
            if (ModelState.IsValid)
            {
                var tenant = _db.Tenant.Find(payment.TenantId);
                tenant.Payments.Add(payment);
                _db.SaveChanges();

            }
            return AddTenantPayment();
        }

        [HttpGet]
        public ActionResult ShowTenantBills()
        {            
            var bill = _db.Tenant
                                .Select(x => new
                                {
                                    TenantId = x.Id,
                                    FirstName = x.FirstName,
                                    MeterReadingDetails = x.MeterReading
                                                           .OrderBy(y=> y.Id)
                                                           .Where(z=>z.DoesBillGenerated != true).FirstOrDefault(),
                                    LastPayment = x.Payments.OrderByDescending(y=>y.Id).FirstOrDefault()
                                }).ToList();

            var billdata = bill.Select(x => new TenantBill
                                                    {
                                                        TenantId = x.TenantId,                                                        
                                                        PreviousMonthReading = (x.MeterReadingDetails==null) ? 0 : x.MeterReadingDetails.PreviousMeterReading,
                                                        CurrentMonthReading = (x.MeterReadingDetails == null) ? 0 : x.MeterReadingDetails.CurrentMeterReading,
                                                        CurrentMonthPayableAmount = (x.MeterReadingDetails == null) ? 0 : x.MeterReadingDetails.AmountPayable,
                                                        LastPaidAmount = (x.LastPayment == null) ? 0 : x.LastPayment.Amount,
                                                        LastPaidAmountDate = (x.LastPayment == null) ? System.DateTime.MinValue : x.LastPayment.DateOfPayment //todo need to display the blank if date is not present
                                                    });

            return View(billdata);
        }
    }
}
