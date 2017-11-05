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

        [HttpGet]
        public ActionResult ShowTenants()
        {
            GetMessage();
            return View(_db.Tenant.ToList());
        }

        [HttpGet]
        public ActionResult AddTenant()
        {
            var listOfMeters = _db.ElectricMeter.Where(em=>em.IsOccupied == false).ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            ViewBag.Meter = listOfMeters;
            GetMessage();

            return View();
        }

        [HttpPost]
        public ActionResult AddTenant(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _db.Tenant.Add(tenant);

                var electricMeter = _db.ElectricMeter.Find(tenant.MeterId);
                electricMeter.IsOccupied = true;
                
                _db.SaveChanges();
            }

            AddMessage("Tenant Added Successfully");

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

            ViewBag.PricePerUnit = _db.ElectricityMeterPerUnitPrices
                                                    .OrderByDescending(y=>y.Value)
                                                    .Select(x=> new SelectListItem{
                                                                                    Value = x.Value.ToString(),
                                                                                    Text = x.Value.ToString()
                                                                        });

            GetMessage();

            return View();
        }

        [HttpPost]
        public ActionResult AddTenantMeterReading(TenantMeterReading tenantMeterReading)
        {

            if (ModelState.IsValid)
            {
                var tenant = _db.Tenant.Find(tenantMeterReading.TenantId);
                tenant.MeterReading.Add(tenantMeterReading);

                var electricMeter = _db.ElectricMeter.Find(tenantMeterReading.MeterId);

                //update meter
                electricMeter.DateOfCurrentMeterReading = tenantMeterReading.DateOfMeterReading;
                electricMeter.CurrentMeterReading = tenantMeterReading.CurrentMeterReading;

                var previousReading = tenant.PreviousReadingDetails.FirstOrDefault();

                //todo add the default values in order to remove null logic
                if(previousReading != null)
                {
                    previousReading.AmountPayable = tenantMeterReading.AmountPayable;
                    previousReading.DateOfPreviousMonthMeterReading = tenantMeterReading.DateOfMeterReading;
                    previousReading.MeterId = tenantMeterReading.MeterId;
                    previousReading.PreviousMeterReading = tenantMeterReading.CurrentMeterReading;
                    previousReading.PerUnitPrice = tenantMeterReading.PerUnitPrice;
                    
                }
                else
                {
                    previousReading = new TenantPreviousReadingDetails
                    {
                        TenantId = tenantMeterReading.TenantId,
                        AmountPayable = tenantMeterReading.AmountPayable,
                        MeterId = tenantMeterReading.MeterId,
                        DateOfPreviousMonthMeterReading = tenantMeterReading.DateOfMeterReading,
                        PreviousMeterReading = tenantMeterReading.CurrentMeterReading,
                        PerUnitPrice = tenantMeterReading.PerUnitPrice
                    };

                    tenant.PreviousReadingDetails.Add(previousReading);
                }

                AddMessage("Meter Reading Added Successfully");

                _db.SaveChanges();
            }

            return RedirectToAction("AddTenantMeterReading");
        }
        

        [HttpGet]
        public ActionResult GetPreviousMeterReading(int Id)
        {
            var tenant = _db.Tenant.Find(Id);
            var meter = _db.ElectricMeter.Find(tenant.MeterId);

            return Json(new
            {
                MeterReading = meter.CurrentMeterReading,
                MeterId = meter.Id,
                MeterName = meter.Name,
                DateOfMeterReading = meter.DateOfCurrentMeterReading
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

            GetMessage();

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
                
                tenant.Payments.Add(payment);

                _db.SaveChanges();

                AddMessage("Payment Added Successfully");

            }
            return RedirectToAction("AddTenantPayment");
        }

        [HttpGet]
        public ActionResult ApproveTenantBills()
        {
            GetMessage();
            return View();
        }

        [HttpPost]
        public ActionResult ApproveTenantBills(IEnumerable<TenantBill> tenantBill)
        {
            var tenantWithPendingBills = _db.Tenant.Where(t => t.MeterReading
                                                                .OrderBy(y => y.Id)
                                                                .Where(z => z.DoesBillGenerated != true).Count() > 0)
                                                                .ToList();

            var billdata = GetPendingBillsOf(tenantWithPendingBills);

            foreach (var tenant in tenantWithPendingBills)
            {
                var currentTenant = _db.Tenant.Where(t=>t.Id.Equals(tenant.Id)).FirstOrDefault();
                var currentTenantBill = billdata.Where(x => x.TenantId == currentTenant.Id).FirstOrDefault();
                currentTenant.AddBill(currentTenantBill);
                currentTenant.MeterReading
                    .OrderBy(x => x.Id)
                    .Where(y => y.DoesBillGenerated != true)
                    .FirstOrDefault().DoesBillGenerated = true;
                _db.SaveChanges();                
            }

            AddMessage("Bills Approved Successfully");

            return RedirectToAction("ApproveTenantBills");
        }

        [HttpGet]
        public ActionResult ViewGeneratedBill()
        {
            ViewBag.Tenant = _db.Tenant.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            });

            return View();
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


            return Json(new { bills = GetGeneratedBills(billList) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBillDetailsOfSelectedTenant(string tenantId)
        {
            var billList = new List<TenantBill>();
            var tenant = _db.Tenant
                            .Where(x => x.Id.ToString() == tenantId)
                            .FirstOrDefault();

            if(tenant == null)
            {
                return Json(new { bills = GetGeneratedBills(billList) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                billList = tenant.Bills.ToList();
                return Json(new { bills = GetGeneratedBills(billList) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetBillDetailsOfSelectedMonth(string month, string year)
        {
            var billList = new List<TenantBill>();
            TenantBill tenantBill;

            var tenantList = _db.Tenant.ToList();

            foreach (Tenant tenant in tenantList)
            {
                tenantBill = _db.Tenant.Find(tenant.Id).GetBillDetailsOf(month, year);

                if (tenantBill != null)
                {
                    billList.Add(tenantBill);
                }
            }

            return Json(new { bills = GetGeneratedBills(billList) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTenants()
        {
            var listOfTenants = _db.Tenant.Select(x => new
                {
                    Id = x.Id,
                    FullName = x.FullName
                }
            ).OrderBy(y=>y.Id).ToList();

            return Json(new { tenants = _db.Tenant.ToList() }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<dynamic> GetGeneratedBills(IEnumerable<TenantBill> bills)
        {
            return bills.Select(x => new
            {
              Id = x.Id,
              TenantName =x.TenantName,
              MonthName = x.MonthName,
              CurrentMonthReadingDate = x.CurrentMonthReadingDate,
              CurrentMonthReading = x.CurrentMonthReading,
              PreviousMonthReadingDate = x.PreviousMonthReadingDate,
              PreviousMonthReading = x.PreviousMonthReading,
              UnitConsumed = x.UnitConsumed,
              PerUnitPrice = x.PerUnitPrice,
              CurrentMonthPayableAmount = x.CurrentMonthPayableAmount,
              PreviousMonthPendingAmount = x.PreviousMonthPendingAmount,
              LastPaidAmountDate = x.LastPaidAmountDate,
              LastPaidAmount = x.LastPaidAmount,
              TotalPayableAmount = x.TotalPayableAmount,
              Year = x.CurrentMonthReadingDate.Year
            })
            .OrderByDescending(y=>y.CurrentMonthReadingDate)
            .ToList();
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

        [HttpGet]
        public ActionResult GetPendingTenantBills()
        {
            var tenantWithPendingBills = _db.Tenant.Where(t => t.MeterReading
                                                                .OrderBy(y => y.Id)
                                                                .Where(z => z.DoesBillGenerated != true).Count() > 0)
                                                                .ToList();

            var billdata = GetPendingBillsOf(tenantWithPendingBills);

            return Json(new { tenantPendingBills = billdata }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ViewPayments()
        {
            ViewBag.Tenant = _db.Tenant.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName
            });

            GetMessage();

            return View();
        }

        [HttpGet]
        public ActionResult EditPayment(int id, int tenantId)
        {
            var tenant = _db.Tenant.Where(x => x.Id == tenantId).FirstOrDefault();
            ViewBag.TenantName = tenant.FullName;
            ViewBag.PaymentType = new SelectList(new[] { PaymentType.Cash, PaymentType.Cheque, PaymentType.BadDebt });

            var payment = tenant.Payments.Where(y=>y.Id == id).FirstOrDefault();

            return View(payment);
        }

        [HttpPost]
        public ActionResult EditPayment(TenantPayment payment)
        {
            if (!ModelState.IsValid)
            {
                return View(payment);
            }

            var tenantPayment = _db.Tenant.
                    Where(x => x.Id == payment.TenantId)
                    .FirstOrDefault()
                    .Payments
                    .Where(y => y.Id == payment.Id)
                    .FirstOrDefault();

            tenantPayment.DateOfPayment = payment.DateOfPayment;
            tenantPayment.Amount = payment.Amount;
            tenantPayment.Comments = payment.Comments;
            tenantPayment.PaymentType = payment.PaymentType;

            _db.SaveChanges();

            AddMessage("Payment updated Successfully");

            return RedirectToAction("ViewPayments");

            
        }

        [HttpGet]
        public ActionResult GetTenantPayments(int tenantId)
        {
            var tenant = _db.Tenant.Where(t => t.Id == tenantId).FirstOrDefault();
            var tenantPayments = tenant.Payments.OrderByDescending(y=>y.DateOfPayment)
                                                .Select(x => new
                                                        {
                                                            Id = x.Id,
                                                            Amount = x.Amount,
                                                            DateOfPayment = x.DateOfPayment,
                                                            Comments = x.Comments,
                                                            TenantId = x.TenantId,
                                                            PaymentType = x.PaymentType.ToString()
                                                        })
                                                        .ToList();
            var totalAmount = tenantPayments.Select(x => x.Amount).DefaultIfEmpty().Sum();
            return Json(new { payments = tenantPayments, tenantName = tenant.FullName, totalAmountPaid = totalAmount }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditTenantDetails(int tenantId)
        {
            var tenant = _db.Tenant.Find(tenantId);
            var listOfMeters = _db.ElectricMeter.Where(em => em.IsOccupied == false || em.Id == tenant.MeterId).ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            
            ViewBag.Meter = listOfMeters;

            return View(tenant);
        }

        [HttpPost]
        public ActionResult EditTenantDetails(Tenant modifiedTenant)
        {
            var tenant = _db.Tenant.Find(modifiedTenant.Id);

            tenant.FirstName = modifiedTenant.FirstName;
            tenant.LastName = modifiedTenant.LastName;
            tenant.PhoneNumber = modifiedTenant.PhoneNumber;            

            if(tenant.MeterId != modifiedTenant.MeterId)
            {
                var oldMeter = _db.ElectricMeter.Find(tenant.MeterId);
                var newMeter = _db.ElectricMeter.Find(modifiedTenant.MeterId);

                oldMeter.IsOccupied = false;

                newMeter.IsOccupied = true;
            }

            tenant.MeterId = modifiedTenant.MeterId;

            _db.SaveChanges();

            AddMessage("Tenant Details updated Successfully");

            return RedirectToAction("ShowTenants");
        }

        [HttpGet]
        public ActionResult AddElectricityUnitPrice()
        {
            GetMessage();
            return View();
        }

        [HttpPost]
        public ActionResult AddElectricityUnitPrice(ElectricityMeterPerUnitPrices electricityMeterPerUnitPrices)
        {
            _db.ElectricityMeterPerUnitPrices.Add(electricityMeterPerUnitPrices);
            _db.SaveChanges();

            AddMessage("Price Added Successfully");

            return RedirectToAction("AddElectricityUnitPrice");
        }

        [HttpGet]
        public ActionResult ViewElectricityUnitPrice()
        {
            GetMessage();
            return View(_db.ElectricityMeterPerUnitPrices.ToList());
        }

        [HttpGet]
        public ActionResult EditElectricityMeterUnitPrice(int id)
        {
            return View(_db.ElectricityMeterPerUnitPrices.Find(id));
        }

        [HttpPost]
        public ActionResult EditElectricityMeterUnitPrice(ElectricityMeterPerUnitPrices electricityMeterPerUnitPrices)
        {
            if(ModelState.IsValid)
            {
                var item = _db.ElectricityMeterPerUnitPrices.Find(electricityMeterPerUnitPrices.Id);
                item.Value = electricityMeterPerUnitPrices.Value;
                _db.SaveChanges();
                AddMessage("Price Updated Successfully");
            }
            return RedirectToAction("ViewElectricityUnitPrice");
        }

        [HttpGet]
        public ActionResult GetTenantAmountReceivable()
        {
            var tenants = _db.Tenant.ToList();

            var amountReceivables = tenants
                                        .Select(x => 
                                            new { TenantName = x.FullName, 
                                                  AmountReceivable = x.GetAmountReceivable() })
                                        .Where(y=>y.AmountReceivable > 0).ToList();
            
            var totalAmountReceivable = amountReceivables.Sum(x=>x.AmountReceivable);

            var listOfAmountReceivables = amountReceivables
                .Select(x => 
                    new { name = x.TenantName, 
                          amount = x.AmountReceivable,
                          percentage = ((x.AmountReceivable / totalAmountReceivable) * 100).ToString("#.##"),
                          y = ((x.AmountReceivable / totalAmountReceivable) * 100)
                    }
                        );


            return Json(new { amountReceivable = listOfAmountReceivables, totalAmount = totalAmountReceivable }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditPendingBill(int Id, int tenantId)
        {
            var tenant = _db.Tenant.Find(tenantId);

            ViewBag.TenantName = tenant.FullName;

            ViewBag.PricePerUnit = _db.ElectricityMeterPerUnitPrices
                                                    .OrderByDescending(y => y.Value)
                                                    .Select(x => new SelectListItem
                                                    {
                                                        Value = x.Value.ToString(),
                                                        Text = x.Value.ToString()
                                                    });


            var meterReading = tenant.MeterReading.Where(x => x.Id == Id).FirstOrDefault();

            return View(meterReading);
        }

        [HttpPost]
        public ActionResult EditPendingBill(TenantMeterReading tenantMeterReading)
        {
            if (ModelState.IsValid)
            {
                var tenant = _db.Tenant.Find(tenantMeterReading.TenantId);
                tenant.UpdatePendingBill(tenantMeterReading);
                _db.SaveChanges();
            }
            

            return RedirectToAction("ApproveTenantBills");
        }

        private static IEnumerable<dynamic> GetPendingBillsOf(List<Tenant> tenantWithPendingBills)
        {
            return tenantWithPendingBills.Select(x => x.GetPendingBillToApprove()).ToList();
        }

        private void AddMessage(string message)
        {
            TempData["message"] = string.IsNullOrEmpty(message) ? " Data Submitted SuccessFully" : " " + message;
        }

        private void GetMessage()
        {
            ViewBag.Message = TempData["message"];
            TempData["message"] = "";
        }
    }
}
