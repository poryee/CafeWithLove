using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class CafeOutlet
    {
        public int cafeOutletId { get; set; }
        public int cafeId { get; set; }
        public string cafeAddress { get; set; }
        public string cafePostalCode { get; set; }
        public string cafeContactNum { get; set; }

        [DefaultValue(0)]
        public int numOfVisit { get; set; }

    }
}