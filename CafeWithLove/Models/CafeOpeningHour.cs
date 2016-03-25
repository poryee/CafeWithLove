using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class CafeOpeningHour
    {
        [Key]
        public int outletOpHourId { get; set; }

        [Required]
        public int cafeOutletId { get; set; }

        [Required]
        public string monday { get; set; }

        [Required]
        public string tuesday { get; set; }

        [Required]
        public string wednesday { get; set; }

        [Required]
        public string thursday { get; set; }

        [Required]
        public string friday { get; set; }

        [Required]
        public string saturday { get; set; }

        [Required]
        public string sunday { get; set; }
    }
}