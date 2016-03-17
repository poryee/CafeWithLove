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
        public int cafeOutletId { get; set; }
        public string monday { get; set; }
        public string tuesday { get; set; }
        public string wednesday { get; set; }
        public string thursday { get; set; }
        public string friday { get; set; }
        public string saturday { get; set; }
        public string sunday { get; set; }
    }
}