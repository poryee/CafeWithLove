using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class ViewModel
    {
        public IEnumerable<CafeDetail> CafeDetailVM { get; set; }
        public ICollection<CafeOutlet> CafeOutletVM { get; set; }
    }
}