using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TenantSystem.Models;

namespace TenantSystem.Controllers
{
    public class HomeController : Controller
    {
        private TenantDBContext _db;

        public HomeController()
        {
            _db = new TenantDBContext();
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

            return View(building);
        }

        [HttpGet]
        public ActionResult AddMeter()
        {            
            ViewBag.MeterType = new SelectList (new[] {ElecticityMeterType.SubMeter, ElecticityMeterType.MainMeter});
            return View();
        }

        [HttpPost]
        public ActionResult AddMeter(ElectricMeter meter)
        {            
            if (ModelState.IsValid)
            {                
                _db.ElectricMeter.Add(meter);
                _db.SaveChanges();                
            }
            return AddMeter();
        }

    }
}
