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
        private TenantDBContext _db;
        private ElectricMeterService _meterService;

        public HomeController()
        {
            _db = new TenantDBContext();
            var session = SqlServerSessionFactory.CreateSessionFactory().OpenSession();
            var meterRepo = new ElectricMeterRepository(session);
            _meterService = new ElectricMeterService(meterRepo);
        }
      
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddBuilding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBuilding(Building building)
        {
            if(ModelState.IsValid)
            {
                _db.Building.Add(building);
                _db.SaveChanges();
                return AddBuilding();
            }

            return RedirectToAction("AddBuilding");
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

                //_db.ElectricMeter.Add(electricMeter);
                //_db.SaveChanges();                
            }
            return RedirectToAction("AddMeter");
        }

        [HttpGet]
        public ActionResult ViewMeters()
        {
            GetMessage();
            return View(_db.ElectricMeter.ToList());
        }

        [HttpGet]
        public ActionResult EditMeter(int Id)
        {
            ViewBag.MeterType = new SelectList(new[] { ElecticityMeterType.SubMeter, ElecticityMeterType.MainMeter });
            var meter = _db.ElectricMeter.Find(Id);

            return View(meter);
        }

        [HttpPost]
        public ActionResult EditMeter(ElectricMeter updatedMeter)
        {
            if (ModelState.IsValid)
            {
                var electricMeter = _db.ElectricMeter.Find(updatedMeter.Id);
                electricMeter.Name = electricMeter.Name;
                electricMeter.MeterType = electricMeter.MeterType;
                electricMeter.IsOccupied = electricMeter.IsOccupied;
                electricMeter.InitialReading = electricMeter.InitialReading;
                electricMeter.DateOfMeterInstalled = electricMeter.DateOfMeterInstalled;
                electricMeter.CurrentMeterReading = electricMeter.CurrentMeterReading;
                electricMeter.DateOfCurrentMeterReading = electricMeter.DateOfCurrentMeterReading;

                AddMessage("Meter Details Updated Successfully");
                
                _db.SaveChanges();
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
