using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Management.Models;

namespace Tour_Management.Controllers
{
    public class SpotController : Controller
    {
        private TravelDbContext db = new TravelDbContext();
        // GET: Spot
        public ActionResult Index()
        {
            return View(db.Spots.ToList());
        }
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpotId, SpotName")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Spots.Add(spot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spot);
        }
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind (Include = "SpotId, SpotName")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spot).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spot);
        }
    }
}