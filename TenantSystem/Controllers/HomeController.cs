using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TenantSystem.BLL;
using TenantSystem.BLL.ViewModel;
using TenantSystem.Infrastructure;
using TenantSystem.Infrastructure.Repository;
using TenantSystem.Models;

namespace TenantSystem.Controllers
{
    public class HomeController : Controller
    {
        private ElectricMeterService _meterService;

        public HomeController()
        {
            var session = SqlServerSessionFactory.CreateSessionFactory().OpenSession();
            var meterRepo = new ElectricMeterRepository(session);
            _meterService = new ElectricMeterService(meterRepo);
        }
      
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddMeter()
        {            
            ViewBag.MeterType = new SelectList (new[] {ElecticityMeterType.SubMeter, ElecticityMeterType.MainMeter});
            return View();
        }

        [HttpPost]
        public ActionResult AddMeter(ElectricMeterViewModel electricMeter)
        {            
            if (ModelState.IsValid)
            {
                electricMeter.DateOfCurrentMeterReading = electricMeter.DateOfMeterInstalled;
                electricMeter.CurrentMeterReading = electricMeter.InitialReading;
                var newMeter = new ElectricMeterViewModel
                {
                    InitialReading = electricMeter.InitialReading,
                    CurrentMeterReading = electricMeter.CurrentMeterReading,
                    DateOfCurrentMeterReading = electricMeter.DateOfCurrentMeterReading,
                    DateOfMeterInstalled = electricMeter.DateOfMeterInstalled,
                    IsOccupied = electricMeter.IsOccupied,
                    MeterType = (int)electricMeter.MeterType,
                    Name = electricMeter.Name
                };

                _meterService.AddMeter(newMeter);
            }
            return RedirectToAction("AddMeter");
        }

        [HttpGet]
        public ActionResult ViewMeters()
        {
            GetMessage();
            return View(_meterService.GetAllMeters());
        }

        [HttpGet]
        public ActionResult EditMeter(int Id)
        {
            ViewBag.MeterType = new SelectList(new[] { ElecticityMeterType.SubMeter, ElecticityMeterType.MainMeter });
            var meter = _meterService.GetMeter(Id);
            return View(meter);
        }

        [HttpPost]
        public ActionResult EditMeter(ElectricMeterViewModel updatedMeter)
        {
            if (ModelState.IsValid)
            {
                _meterService.UpdateMeter(updatedMeter);
                AddMessage("Meter Details Updated Successfully");
            }
            return RedirectToAction("ViewMeters");
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
