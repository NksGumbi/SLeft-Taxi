using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SLeft_Taxi.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace SLeft_Taxi.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private TaxiReservationSystemContext db = new TaxiReservationSystemContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var ticket = db.Ticket.Include(t => t.Taxi).Include(t => t.Payment).Include(t => t.Schedule).Where(t => t.UserId == User.Identity.Name);
            return View(ticket.ToList());
        }

        //GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Reservations/Book
        public ActionResult Book(int sid, int tid, DateTime doj, string name)
        {

            Ticket model = new Ticket()
            {
                taxiId = tid,
                scheduleId = sid,
                dateOfJourney = doj.Date,
                //UserId = User.Identity.Name
            };
            ViewBag.Taxiname = name;

            return View(model);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteBooking([Bind(Include = "paymentMode,totalAmount,bankDetails")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payment.Add(payment);
                db.SaveChanges();
            }

            Ticket ticket = TempData["Ticket"] as Ticket;
            ticket.UserId = User.Identity.Name;
            ticket.paymentId = payment.paymentId;

            db.Ticket.Add(ticket);
            db.SaveChanges();

            Schedule schedule = db.Schedule.Find(ticket.scheduleId);
            
            db.Entry(schedule).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment([Bind(Include = "taxiId,scheduleId,paymentId,dateOfJourney,passengerName,gender,phoneNumber,address,emergencyContact")] Ticket ticket)
        {
            TempData["Ticket"] = ticket;
           

            ticket.seatNo = db.Ticket.Include(t => t.ticketId).Where(t => t.paymentId == ticket.paymentId).Count() + 1;
            Payment payment = new Payment()
            {
                totalAmount = getcostofticket(ticket.scheduleId)
            };

            return View(payment);
        }

        private double getcostofticket(int scheduleId)
        {
            double cost = 0.00;
            Schedule s = new Schedule();
            var query = "SELECT cost" + cost + " FROM Schedule where scheduleId=" + scheduleId;
            string cString = ConfigurationManager.ConnectionStrings["TaxiReservationSystemContext"].ConnectionString;
            //string cString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog = aspnet-TaxiReservationSystem-20151101020301; Integrated Security = True";
            using (SqlConnection c = new SqlConnection(cString))
            {
                c.Open();
                using (SqlCommand cmd = new SqlCommand(query, c))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            cost = rdr.GetDouble(rdr.GetOrdinal("cost"));
                        }
                    }
                }
            }
            return (cost);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.taxiId = new SelectList(db.Taxi, "taxiId", "TaxiName", ticket.taxiId);
            ViewBag.paymentId = new SelectList(db.Payment, "paymentId", "paymentMode", ticket.paymentId);
            ViewBag.scheduleId = new SelectList(db.Schedule, "scheduleId", "source", ticket.scheduleId);
            return View(ticket);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ticketId,UserId,taxiId,scheduleId,paymentId,dateOfJourney,seatNo,passengerName,phoneNumber,address,emergencyContact,travelclass")] Ticket ticket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(ticket).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.taxiId = new SelectList(db.Taxi, "taxiId", "TaxiName", ticket.taxiId);
        //    ViewBag.paymentId = new SelectList(db.Payment, "paymentId", "paymentMode", ticket.paymentId);
        //    ViewBag.scheduleId = new SelectList(db.Schedule, "scheduleId", "source", ticket.scheduleId);
        //    return View(ticket);
        //}

        // GET: Reservations/Delete/5
        public ActionResult CancelTicket(int id)
        {

            Ticket ticket = db.Ticket.Find(id);
            //ViewBag.cancelexception = "true";
            //if (ticket.dateOfJourney.CompareTo(DateTime.Today) > 0)
            //{
            //    ViewBag.cancelexception = "false";
            //}
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("CancelTicket")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            db.Ticket.Remove(ticket);
            db.SaveChanges();
            Schedule schedule = db.Schedule.Find(ticket.scheduleId);
           
            db.Entry(schedule).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}