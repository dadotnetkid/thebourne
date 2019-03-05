using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace thebourne.Controllers
{
    public class HomeController : Controller
    {
        private thebourne.Models.TheBournEntity db = new Models.TheBournEntity();


        public ActionResult Index()
        {
            //ViewBag.dateofReservation = dateofReservation;
            var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ViewBag.dateofReservation = dt;
            return View(db.Seats);
        }
        [HttpPost]
        public ActionResult Index(DateTime? dateofReservation)
        {
            ViewBag.dateofReservation = dateofReservation;
            return View(db.Seats);
        }
        [HttpPost]
        public ActionResult Reserve(DateTime? dateofReservation, int? seatId)
        {
            var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            db.Reservations.Add(new Models.Reservations()
            {
                ReservationDate = dateofReservation ?? dt,
                SeatId = seatId,
                UserId = User.Identity.GetUserId()
            });
            db.AuditTrails.Add(new Models.AuditTrails() { UserId = User.Identity.GetUserId(), Action = $"Reserve a seat no. {db.Seats.Find(seatId)?.TableName}", DateCreated = DateTime.Now });
            db.SaveChanges();
            ViewBag.dateofReservation = dateofReservation;
            return View("index", db.Seats);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}