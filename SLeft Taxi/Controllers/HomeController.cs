using SLeft_Taxi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SLeft_Taxi.Controllers
{
    public class HomeController : Controller
    {
        private TaxiReservationSystemContext db = new TaxiReservationSystemContext();
        public ActionResult Index()
        {

            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            var srclist = new List<Schedule>();
            var destlist = new List<Schedule>();
            //string cString = ConfigurationManager.ConnectionStrings["TaxiReservationSystemContext"].ConnectionString;
            //string cString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog = aspnet-TaxiReservationSystem-20151101020301; Integrated Security = True";
            //using (SqlConnection c = new SqlConnection(cString))
            //{
            //    c.Open();
            //    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT source FROM Schedule", c))
            //    {
            //        using (SqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                srclist.Add(new Schedule
            //                {
            //                    source = rdr.GetString(0)
            //                });
            //            }
            //        }
            //    }
            //    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT dest FROM Schedule", c))
            //    {
            //        using (SqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                destlist.Add(new Schedule
            //                {
            //                    destination = rdr.GetString(0)
            //                });
            //            }
            //        }
            //    }
            //}

            ViewBag.source = new SelectList(srclist, "source", "source");
            ViewBag.dest = new SelectList(destlist, "destination", "destination");

            //ViewBag.source = new SelectList(db.Schedule, "source","source");
            //ViewBag.dest = new SelectList(db.Schedule, "destination", "destination");
            //ViewBag.date = new DateTime();
            ViewBag.date = new DateTime();
            return View();
        }

        [HttpPost]
        public ActionResult SearchResults(string source, string destination, DateTime dateOfJourney)
        {

            //var srclist = new List<Schedule>();
            //var destlist = new List<Schedule>();
            //string cString = ConfigurationManager.ConnectionStrings["TaxiReservationSystemContext"].ConnectionString;
            ////string cString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog = aspnet-FlightReservationSystem-20151101020301; Integrated Security = True";
            //using (SqlConnection c = new SqlConnection(cString))
            //{
            //    c.Open();
            //    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Schedule WHERE source = @source and dest = @dest and scheduleDate = @dateOfJourney", c))
            //    {
            //        using (SqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                srclist.Add(new Schedule
            //                {
            //                    source = rdr.GetString(0)
            //                });
            //            }
            //        }
            //    }
            //    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT dest FROM Schedule", c))
            //    {
            //        using (SqlDataReader rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                destlist.Add(new Schedule
            //                {
            //                    dest = rdr.GetString(0)
            //                });
            //            }
            //        }
            //    }
            //}
            ////ViewData["source"] = source;
            ////ViewData["destination"] = destination;
            ////ViewData["dateOfJourney"] = dateOfJourney;
            ViewBag.Source = source;
            ViewBag.Destination = destination;
            ViewBag.ScheduleMessage = "";
            if (DateTime.Compare(dateOfJourney, DateTime.Today) > 0)
            {
                var data = from s in db.Schedule
                           where s.source == source && s.destination == destination && s.scheduleDate == dateOfJourney
                           select s;
                if (data.ToList().Count() == 0)
                {
                    ViewBag.ScheduleMessage = "No Taxi rides on the entered date, below are the rides from other days";
                    data = from s in db.Schedule
                           where s.source == source && s.destination == destination && DateTime.Compare(s.scheduleDate, DateTime.Today) > 0
                           select s;
                }
                return View(data.ToList());
            }
            else
            {

                //var data = from s in db.Schedule
                //           where s.source == source && s.destination == dest && s.scheduleDate.CompareTo(DateTime.Today) >= 0 && TimeSpan.Compare(s.depatureTime,DateTime.Now.TimeOfDay) > 0
                //           select s;
                var data = from s in db.Schedule
                           where s.source == source && s.destination == destination && DateTime.Compare(s.scheduleDate, DateTime.Today) >= 0
                           select s;
                if (dateOfJourney.CompareTo(DateTime.Today) == 0)
                    ViewBag.ScheduleMessage = "Cannot book Rides today for requested source and destination. Rides from requested source to destination are listed below";
                else
                    ViewBag.ScheduleMessage = "Entered past date, Rides from requested source to destination are listed below";
                return View(data.ToList());

            }

        }


        public ActionResult About()
        {


            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}