using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class OutletViewModel
    {
        public CafeDetail CafeDetailVM { get; set; }
        public CafeOutlet CafeOutletVM { get; set; }
        public CafeOpeningHour CafeOpeningHourVM { get; set; }
    }
}