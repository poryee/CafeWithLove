using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Monday")]
        [Required]
        public string monday { get; set; }

        [DisplayName("Tuesday")]
        [Required]
        public string tuesday { get; set; }

        [DisplayName("Wednesday")]
        [Required]
        public string wednesday { get; set; }

        [DisplayName("Thursday")]
        [Required]
        public string thursday { get; set; }

        [DisplayName("Friday")]
        [Required]
        public string friday { get; set; }

        [DisplayName("Saturday")]
        [Required]
        public string saturday { get; set; }

        [DisplayName("Sunday")]
        [Required]
        public string sunday { get; set; }
    }
}