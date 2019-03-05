using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using thebourne.Models;

namespace thebourne.Controllers
{
    public class ReservationsController : Controller
    {
        private TheBournEntity db = new TheBournEntity();
        private string UserId => User.Identity.GetUserId();
        public ActionResult Index()
        {
            ViewBag.SuccessPayment = Request.Params["SuccessPayment"];
            ViewBag.ErrorPaypal = Request.Params["ErrorPaypal"];
            return View(db.Reservations.Where(m => m.UserId == UserId));
        }
        [Authorize]
        [HttpPost]
        public ActionResult CancelReservation(int? id)
        {
            var reservation = db.Reservations.Find(id);
            reservation.isCancel = true;
            db.AuditTrails.Add(new Models.AuditTrails() { UserId = User.Identity.GetUserId(), Action = $"Cancel a Reservation {db.Reservations.Find(id)?.Seats.TableName}", DateCreated = DateTime.Now });
            db.SaveChanges();
            return RedirectToAction("index", "reservations");
        }

        public ActionResult CustomersReservation()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridView1Partial()
        {
            var model = db.Reservations.Include("AspNetUsers").Include("Seats").ToList();
            return PartialView("_GridView1Partial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))]
            thebourne.Models.Reservations item)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    // Insert here a code to insert the new item in your model
                    item.OrNumber = (new Random().Next(1000000)).ToString();
                    item.isPaid = true;
                    item.ReservedBy = User.Identity.GetUserId();
                    db.Reservations.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    ViewData["Model"] = item;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }


            var model = db.Reservations.Include("AspNetUsers").Include("Seats").ToList();
            return PartialView("_GridView1Partial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] thebourne.Models.Reservations item)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    db.Reservations.Attach(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    ViewData["Model"] = item;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = db.Reservations.Include("AspNetUsers").Include("Seats").ToList();
            return PartialView("_GridView1Partial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {
    
            if (Id >= 0)
            {
                try
                {
                    db.Reservations.Remove(db.Reservations.Find(Id));
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = db.Reservations.Include("AspNetUsers").Include("Seats").ToList();
            return PartialView("_GridView1Partial", model);
        }

        public ActionResult AddEditReservationPartial(int? reservationId)
        {
            var model = db.Reservations.Find(reservationId);
            return PartialView("AddEditReservationPartial", model);
        }
    }
}