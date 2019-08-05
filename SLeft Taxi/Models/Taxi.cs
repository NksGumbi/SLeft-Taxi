using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SLeft_Taxi.Models
{
    [Table("Taxi")]
    public class Taxi
    {
        [Required]
        [Key]
        public int taxiId { get; set; }
        [Required]
        public string taxiName { get; set; }

        public int AvailableSeats { get; set; }


        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}