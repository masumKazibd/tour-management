using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tour_Management.Models;

namespace Tour_Management.Controllers.Home
{
    public class HomeController : Controller
    {
        private TravelDbContext db = new TravelDbContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.tClient = db.Clients.Count();
            ViewBag.tSpot = db.Spots.Count();
            ViewBag.tBookings = db.BookingEntries.Count();
            return View();
        }
    }
}