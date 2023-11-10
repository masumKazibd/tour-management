using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Tour_Management.Models;

namespace Tour_Management.Controllers
{
    public class SpotController : Controller
    {
        private TravelDbContext db = new TravelDbContext();
        // GET: Spot
        public ActionResult Index(int page=1)
        {
            ViewBag.totalPages = (int)Math.Ceiling((double)db.Spots.Count() / 5);
            ViewBag.currentPage = page;
            return View(db.Spots.OrderBy(x=>x.SpotId).Skip((page-1)*5).Take(5).ToList());
            //return View(db.Spots.ToList());
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DoDelete(int id)
        {
            Spot spot = db.Spots.Find(id);
            db.Spots.Remove(spot);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DoDelete(int id)
        //{
        //    Sports sports = db.Sports.Find(id);
        //    db.Sports.Remove(sports);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}