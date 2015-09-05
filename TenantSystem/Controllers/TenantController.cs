using System;
using System.Collections.Generic;
using System.Globalization;
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
            var listOfMeters = _db.ElectricMeter.ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
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

            return RedirectToAction("AddTenant");
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

            return RedirectToAction("AddTenantMeterReading");
        }

        [HttpGet]
        public ActionResult GetPreviousMeterReading(int Id)
        {
            var tenant = _db.Tenant.Find(Id);
            var prevReading = tenant.MeterReading
                                    .OrderByDescending(x => x.Id)
                                    .FirstOrDefault();

            return Json(new
            {
                MeterReading = (prevReading == null) ? 0 : prevReading.CurrentMeterReading,
                MeterId = tenant.MeterId,
                DateOfMeterReading = (prevReading == null) ? DateTime.MinValue : prevReading.DateOfMeterReading.Date
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddTenantPayment()
        {
            ViewBag.Tenant = _db.Tenant.Select(x => new SelectListItem
                                                {
                                                    Value = x.Id.ToString(),
                                                    Text = x.FirstName
                                                });

            ViewBag.PaymentType = new SelectList(new[] { PaymentType.Cash, PaymentType.Cheque, PaymentType.BadDebt });

            return View();
        }

        [HttpPost]
        public ActionResult AddTenantPayment(TenantPayment payment)
        {
            if (ModelState.IsValid)
            {
                var tenant = _db.Tenant.Find(payment.TenantId);
                var meterReading = tenant.MeterReading
                                    .OrderByDescending(x => x.Id)
                                    .Where(y => y.DoesBillGenerated == true)
                                                .FirstOrDefault();

                if (meterReading.PaymentId > 0)
                {
                    throw new Exception("payment already made");
                }

                tenant.Payments.Add(payment);

                _db.SaveChanges();
                meterReading.PaymentId = tenant.Payments
                                                .OrderByDescending(x => x.Id)
                                                .Select(y => y.Id).FirstOrDefault();
                _db.SaveChanges();

            }
            return RedirectToAction("AddTenantPayment");
        }

        [HttpGet]
        public ActionResult ApproveTenantBills()
        {
            var datebll = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            var bill = _db.Tenant
                                .Select(x => new
                                {
                                    TenantId = x.Id,
                                    Name = x.FullName,
                                    MeterReadingDetails = x.MeterReading
                                                           .OrderBy(y => y.Id)
                                                           .Where(z => z.DoesBillGenerated != true).FirstOrDefault(),
                                    LastPayment = x.Payments.OrderByDescending(y => y.Id).FirstOrDefault(),
                                    PreviousMonthPendingAmount = x.MeterReading.OrderByDescending(z => z.Id).Skip(1)
                                                                  .Select(y => y.AmountPayable).DefaultIfEmpty().Sum() -
                                                                  x.Payments.Select(y => y.Amount).DefaultIfEmpty().Sum()
                                }).ToList();

            var billdata = bill
                            .Select(x => new TenantBill
                                                    {
                                                        TenantId = x.TenantId,
                                                        PreviousMonthReading = (x.MeterReadingDetails == null) ? 0 : x.MeterReadingDetails.PreviousMeterReading,
                                                        PreviousMonthReadingDate = (x.MeterReadingDetails == null) ? DateTime.MinValue : x.MeterReadingDetails.DateOfPreviousMonthMeterReading,
                                                        CurrentMonthReading = (x.MeterReadingDetails == null) ? 0 : x.MeterReadingDetails.CurrentMeterReading,
                                                        CurrentMonthPayableAmount = (x.MeterReadingDetails == null) ? 0 : x.MeterReadingDetails.AmountPayable,
                                                        CurrentMonthReadingDate = (x.MeterReadingDetails == null) ? DateTime.MinValue : x.MeterReadingDetails.DateOfMeterReading,
                                                        LastPaidAmount = (x.LastPayment == null) ? 0 : x.LastPayment.Amount,
                                                        LastPaidAmountDate = (x.LastPayment == null) ? DateTime.MinValue : x.LastPayment.DateOfPayment,//todo need to display the blank if date is not present
                                                        PreviousMonthPendingAmount = x.PreviousMonthPendingAmount,
                                                        TotalPayableAmount = (x.MeterReadingDetails == null) ? 0 : x.PreviousMonthPendingAmount + x.MeterReadingDetails.AmountPayable,
                                                        TenantName = x.Name,
                                                        PerUnitPrice = (x.MeterReadingDetails == null) ? 0 : x.MeterReadingDetails.PerUnitPrice,
                                                        MonthName = (x.MeterReadingDetails == null) ? null : CultureInfo.CurrentCulture.DateTimeFormat
                                                                                                             .GetMonthName(x.MeterReadingDetails.DateOfMeterReading.Month)
                                                    });

            return View(billdata);
        }

        [HttpPost]
        public ActionResult ApproveTenantBills(IEnumerable<TenantBill> tenantBill)
        {

            var bill = _db.Tenant
                                .Select(x => new
                                {
                                    TenantId = x.Id,
                                    Name = x.FullName,
                                    MeterReadingDetails = x.MeterReading
                                                           .OrderBy(y => y.Id)
                                                           .Where(z => z.DoesBillGenerated != true).FirstOrDefault(),
                                    LastPayment = x.Payments
                                                    .Where(z => z.PaymentType != PaymentType.BadDebt)
                                                    .OrderByDescending(y => y.Id)
                                                    .FirstOrDefault(),
                                    PreviousMonthPendingAmount = x.MeterReading.OrderByDescending(z => z.Id).Skip(1)
                                                                  .Select(y => y.AmountPayable).DefaultIfEmpty().Sum() -
                                                                  x.Payments.Select(y => y.Amount).DefaultIfEmpty().Sum()
                                }).ToList();

            var billdata = bill
                            .Select(x => new TenantBill
                            {
                                TenantId = x.TenantId,
                                PreviousMonthReading = (x.MeterReadingDetails == null) ? 0 : x.MeterReadingDetails.PreviousMeterReading,
                                CurrentMonthReading = (x.MeterReadingDetails == null) ? 0 : x.MeterReadingDetails.CurrentMeterReading,
                                CurrentMonthPayableAmount = (x.MeterReadingDetails == null) ? 0 : x.MeterReadingDetails.AmountPayable,
                                CurrentMonthReadingDate = (x.MeterReadingDetails == null) ? DateTime.MinValue : x.MeterReadingDetails.DateOfMeterReading,
                                LastPaidAmount = (x.LastPayment == null) ? 0 : x.LastPayment.Amount,
                                LastPaidAmountDate = (x.LastPayment == null) ? DateTime.MinValue : x.LastPayment.DateOfPayment,//todo need to display the blank if date is not present
                                PreviousMonthPendingAmount = x.PreviousMonthPendingAmount,
                                TotalPayableAmount = x.PreviousMonthPendingAmount + x.MeterReadingDetails.AmountPayable,
                                TenantName = x.Name,
                                PerUnitPrice = x.MeterReadingDetails.PerUnitPrice,
                                MonthName = (x.MeterReadingDetails == null) ? null : CultureInfo.CurrentCulture.DateTimeFormat
                                                                                     .GetMonthName(x.MeterReadingDetails.DateOfMeterReading.Month)
                            });

            for (int i = 1; i <= _db.Tenant.Count(); i++)
            {
                var currentTenant = _db.Tenant.Find(i);
                var currentTenantBill = billdata.Where(x => x.TenantId == currentTenant.Id).FirstOrDefault();
                currentTenant.Bills.Add(currentTenantBill);
                currentTenant.MeterReading
                    .OrderBy(x => x.Id)
                    .Where(y => y.DoesBillGenerated != true)
                    .FirstOrDefault().DoesBillGenerated = true;

                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ViewGeneratedBill()
        {
            List<TenantBill> bills = new List<TenantBill>();

            for (int i = 1; i <= _db.Tenant.Count(); i++)
            {
                var currentTenant = _db.Tenant.Find(i);
                var bill = currentTenant.Bills.OrderByDescending(x => x.Id).FirstOrDefault();
                if (bill != null)
                {
                    bills.Add(bill);
                }
            }

            ViewBag.Tenant = _db.Tenant.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            });

            return View(bills);
        }

        [HttpGet]
        public ActionResult GetBillDetailsOfSelectedMonthAndTenant(string month, string year, string tenantId)
        {
            var billList = new List<TenantBill>();

            var billTempList = _db.Tenant
                                .Where(x => x.Id.ToString() == tenantId)
                                .FirstOrDefault()
                                .Bills.Where(x => (x.CurrentMonthReadingDate.Month == int.Parse(month))
                                            && (x.CurrentMonthReadingDate.Year.ToString() == year))
                                .FirstOrDefault(); ;

            if (billTempList != null)
            {
                billList.Add(billTempList);
            }

            return Json(new { bills = billList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBillDetailsOfSelectedTenant(string tenantId)
        {
            var billList = new List<TenantBill>();
            billList = _db.Tenant
                            .Where(x => x.Id.ToString() == tenantId)
                            .FirstOrDefault()
                            .Bills.ToList();

            return Json(new { bills = billList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBillDetailsOfSelectedMonth(string month, string year)
        {
            var billList = new List<TenantBill>();
            TenantBill tenantBill;

            foreach (Tenant item in _db.Tenant)
            {
                tenantBill = item.Bills
                                .Where(x => (x.CurrentMonthReadingDate.Month == int.Parse(month))
                                            && (x.CurrentMonthReadingDate.Year.ToString() == year))
                                .FirstOrDefault();

                if (tenantBill != null)
                {
                    billList.Add(tenantBill);
                }
            }

            return Json(new { bills = billList }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ViewTenantHistory()
        {
            _db.Tenant.Where(x => x.Id == 1).FirstOrDefault().Payments.ToList();

            ViewBag.Tenant = _db.Tenant.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            });

            return View();
        }

        [HttpGet]
        public ActionResult GetHistoryOfSelectedTenant(string tenantId)
        {
            var billList = new List<TenantPayment>();

            var tenant = _db.Tenant.Where(x => x.Id.ToString() == tenantId).FirstOrDefault();

            var tenantBills = tenant.Bills.ToList();
            var meterReadings = tenant.MeterReading.ToList();
            var payment = tenant.Payments.ToList();

            var result = from bill in tenantBills
                         select new
                         {
                             TenantId = bill.TenantId,
                             PaymentAmount = bill.LastPaidAmount,
                             PaymentDate = bill.LastPaidAmountDate,
                             CurrentMonthMeterReading = bill.CurrentMonthReading,
                             CurrentMonthMeterReadingDate = bill.CurrentMonthReadingDate,
                             PreviousMonthMeterReadingDate = bill.PreviousMonthReadingDate,
                             PreviousMonthMeterReading = bill.PreviousMonthReading,
                             AmountPayable = bill.CurrentMonthPayableAmount,
                             PerUnitPrice = bill.PerUnitPrice,
                             TotalAmountPayable = bill.TotalPayableAmount
                         };

            return Json(new { bills = result, TenantName = tenant.FullName }, JsonRequestBehavior.AllowGet);
        }
    }
}
