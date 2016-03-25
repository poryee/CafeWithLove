using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class CafeOutlet
    {
        [Required]
        public int cafeOutletId { get; set; }

        [Required]
        public int cafeId { get; set; }

        [Required]
        public string cafeAddress { get; set; }

        [Required]
        public string cafePostalCode { get; set; }

        public string cafeContactNum { get; set; }

        [DefaultValue(0)]
        public int numOfVisit { get; set; }

        public CafeOutlet() {
            numOfVisit = 0;
        }
    }
}