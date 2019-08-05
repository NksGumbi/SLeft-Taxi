using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SLeft_Taxi.Models
{
    public class TaxiReservationSystemContext : DbContext
    {
        public TaxiReservationSystemContext() : base("TaxiReservationSystemContext")
        {
        }

        public DbSet<Taxi> Taxi { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Payment> Payment { get; set; }

        //public System.Data.Entity.DbSet<TaxiReservationSystem.Models.Taxi> Taxi { get; set; }
        //public System.Data.Entity.DbSet<TaxiReservationSystem.Models.Schedule> Schedule { get; set; }
        //public System.Data.Entity.DbSet<TaxiReservationSystem.Models.Ticket> Ticket { get; set; }
        //public System.Data.Entity.DbSet<TaxiReservationSystem.Models.Payment> Payment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }
}

