using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SLeft_Taxi.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        [Key]
        public int scheduleId { get; set; }

        [ForeignKey("Taxi")]
        public int taxiId { get; set; }

        [Required]
        public string source { get; set; }

        [Required]
        public int sourceId { get; set; }

        [Required]
        public string destination { get; set; }

        [Required]
        public int destinationId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime scheduleDate { get; set; }

        [Required]
        public TimeSpan depatureTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime arrivalDate { get; set; }

        [Required]
        public TimeSpan arrivalTime { get; set; }

        [Required]
        public string status { get; set; }

        [Required]
        public double cost { get; set; }

        [Required]
        public int AvailSeats { get; set; }

        public virtual Taxi Taxi { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }


    }
}