using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class CafeViewModel
    {
        public CafeDetail CafeDetailVM { get; set; }
        public ICollection<CafeOutlet> CafeOutletVM { get; set; }
        public ICollection<CafeOpeningHour> CafeOpeningHourVM { get; set; }
    }
}