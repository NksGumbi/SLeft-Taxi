using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLeft_Taxi.Models
{
    public class DbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TaxiReservationSystemContext>
    {
        protected override void Seed(TaxiReservationSystemContext context)
        {
            var Taxi = new List<Taxi>
            {
                new Taxi {taxiId = 1, taxiName = "Quantum", AvailableSeats = 15},
                new Taxi {taxiId = 2, taxiName = "Quantum", AvailableSeats = 15},
                new Taxi {taxiId = 3, taxiName = "Siyaya", AvailableSeats = 15},
                new Taxi {taxiId = 4, taxiName = "Quantum", AvailableSeats = 15},
                new Taxi {taxiId = 5, taxiName = "Siyaya", AvailableSeats = 15}

            };

            Taxi.ForEach(s => context.Taxi.Add(s));
            context.SaveChanges();

            var schedule = new List<Schedule>
            {
                new Schedule { scheduleId = 1,  taxiId =1, source = "Pinetown", sourceId = 11, destination = "Steve_Biko_terminal", destinationId = 22, scheduleDate = new DateTime(2017,10, 13) , depatureTime = new TimeSpan(7,10, 18) ,arrivalDate = new DateTime(2017,10, 13) ,arrivalTime = new TimeSpan(7, 30, 50), status = "onschedule", AvailSeats = 15, cost = 12.00 },
                new Schedule { scheduleId = 2,  taxiId =1, source = "Steve_Biko_terminal", sourceId = 22, destination = "Pinetown", destinationId = 11, scheduleDate = new DateTime(2017,10, 13) , depatureTime = new TimeSpan(17,10, 18) ,arrivalDate = new DateTime(2017,10, 13) ,arrivalTime = new TimeSpan(17, 30, 50), status = "onschedule", AvailSeats = 15, cost = 12.00 },
                new Schedule { scheduleId = 3,  taxiId =2, source = "Chartsworth", sourceId = 12, destination = "Steve_Biko_terminal", destinationId = 13, scheduleDate = new DateTime(2017,10, 13) , depatureTime = new TimeSpan(7,10, 18) ,arrivalDate = new DateTime(2017,10, 13) ,arrivalTime = new TimeSpan(7, 30, 50), status = "onschedule", AvailSeats = 15, cost = 16.00 },
                new Schedule { scheduleId = 4,  taxiId =2, source = "Steve_Biko_terminal", sourceId = 12, destination = "Chartsworth", destinationId = 13, scheduleDate = new DateTime(2017,10, 13) , depatureTime = new TimeSpan(17,10, 18) ,arrivalDate = new DateTime(2017,10, 13) ,arrivalTime = new TimeSpan(17, 30, 50), status = "onschedule", AvailSeats = 15, cost = 16.00 },
                new Schedule { scheduleId = 5,  taxiId =3, source = "Mlazi", sourceId = 12, destination = "Steve_Biko_terminal", destinationId = 13, scheduleDate = new DateTime(2017,10, 13) , depatureTime = new TimeSpan(7,10, 18) ,arrivalDate = new DateTime(2017,10, 13) ,arrivalTime = new TimeSpan(7, 30, 50), status = "onschedule", AvailSeats = 15, cost = 14.00 },
                new Schedule { scheduleId = 6,  taxiId =3, source = "Steve_Biko_terminal", sourceId = 12, destination = "Mlazi", destinationId = 13, scheduleDate = new DateTime(2017,10, 13) , depatureTime = new TimeSpan(17,10, 18) ,arrivalDate = new DateTime(2017,10, 13) ,arrivalTime = new TimeSpan(17, 30, 50), status = "onschedule", AvailSeats = 15, cost = 14.00 },
                new Schedule { scheduleId = 7,  taxiId =4, source = "Bluff", sourceId = 12, destination = "Steve_Biko_terminal", destinationId = 13, scheduleDate = new DateTime(2017,10, 13) , depatureTime = new TimeSpan(7,10, 18) ,arrivalDate = new DateTime(2017,10, 13) ,arrivalTime = new TimeSpan(7, 30, 50), status = "onschedule", AvailSeats = 15, cost = 13.00 },
                new Schedule { scheduleId = 8,  taxiId =4, source = "Steve_Biko_terminal", sourceId = 12, destination = "Bluff", destinationId = 13, scheduleDate = new DateTime(2017,10, 13) , depatureTime = new TimeSpan(17,10, 18) ,arrivalDate = new DateTime(2017,10, 13) ,arrivalTime = new TimeSpan(17, 30, 50), status = "onschedule", AvailSeats = 15, cost = 13.00 },
                new Schedule { scheduleId = 9,  taxiId =5, source = "CBD", sourceId = 12, destination = "Steve_Biko_terminal", destinationId = 13, scheduleDate = new DateTime(2017,10, 13) , depatureTime = new TimeSpan(7,10, 18) ,arrivalDate = new DateTime(2017,10, 13) ,arrivalTime = new TimeSpan(7, 30, 50), status = "onschedule", AvailSeats = 15, cost = 10.00 },
                new Schedule { scheduleId = 10,  taxiId =5, source = "Steve_Biko_terminal", sourceId = 12, destination = "CBD", destinationId = 13, scheduleDate = new DateTime(2017,10, 13) , depatureTime = new TimeSpan(17,10, 18) ,arrivalDate = new DateTime(2017,10, 13) ,arrivalTime = new TimeSpan(17, 30, 50), status = "onschedule", AvailSeats = 15, cost = 10.00 }

            };

            schedule.ForEach(s => context.Schedule.Add(s));
            context.SaveChanges();


            var payment = new List<Payment>
            {
            new Payment{ paymentId = 1, paymentMode = "Cash", totalAmount = 200.00},
            new Payment{ paymentId = 2, paymentMode = "Visa", bankDetails = "eBank", totalAmount = 120.00},
            new Payment{ paymentId = 3, paymentMode = "Master", bankDetails = "eBank", totalAmount = 20.00},
            new Payment{ paymentId = 4, paymentMode = "Master", bankDetails = "eBank", totalAmount = 50.00}

            };

            payment.ForEach(s => context.Payment.Add(s));
            context.SaveChanges();

            var ticket = new List<Ticket>
            {
            new Ticket{ticketId = 1, UserId = "nks.gumbi@gmail.com", taxiId = 1, scheduleId = 1, paymentId = 1, dateOfJourney = new DateTime(2017,10,10), seatNo = 1, passengerName = "Nkosikhona", gender = 'M', phoneNumber = "0712345678", address = "Glenmore. Dbn", emergencyContact = "none" },
            new Ticket{ticketId = 2, UserId = "nks.gumbi@gmail.com", taxiId = 1, scheduleId = 1, paymentId = 1, dateOfJourney = new DateTime(2017,10,10), seatNo = 2, passengerName = "Jane", gender = 'F', phoneNumber = "0812345678", address = "South Beach. Dbn", emergencyContact = "0812345678" },
            new Ticket{ticketId = 3, UserId = "2", taxiId = 3, scheduleId = 10, paymentId = 2, dateOfJourney = new DateTime(2017,10,09), seatNo = 3, passengerName = "Alex", gender = 'M', phoneNumber = "0812345678", address = "Durban North. Dbn", emergencyContact = "0812345678" },
            new Ticket{ticketId = 4, UserId = "3", taxiId = 3, scheduleId = 14, paymentId = 3, dateOfJourney = new DateTime(2017,09,23), seatNo = 4, passengerName = "Demo", gender = 'F', phoneNumber = "0812345678", address = "Florida rd. Dbn", emergencyContact = "0812345678" },
            new Ticket{ticketId = 5, UserId = "4", taxiId = 4, scheduleId = 19, paymentId = 4, dateOfJourney = new DateTime(2017,09,30), seatNo = 5, passengerName = "Test", gender = 'M', phoneNumber = "0812345678", address = "Mlazi. Dbn", emergencyContact = "0812345678" }
            };
            ticket.ForEach(s => context.Ticket.Add(s));
            context.SaveChanges();

        }
    }
}