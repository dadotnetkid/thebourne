using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace thebourne.Models
{
    public partial class Reservations
    {
    }

    public class ReservationViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int SeatId { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        [Required]
        public decimal AmountPaid { get; set; }
    }
}