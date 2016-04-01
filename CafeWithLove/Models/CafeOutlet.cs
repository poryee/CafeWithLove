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

        [DisplayName("Cafe Address")]
        [Required]
        public string cafeAddress { get; set; }

        [DisplayName("Cafe Postal Code")]
        [Required]
        [StringLength(6)]
        [RegularExpression("(\\d{6})", ErrorMessage = "Invalid postal code")]
        public string cafePostalCode { get; set; }

        [DisplayName("Cafe Contact Number")]
        [RegularExpression("(\\d{4} \\d{4}|-)", ErrorMessage = "Invalid phone number. Example of number: 9123 4567. If no number: -")]
        public string cafeContactNum { get; set; }

        [DefaultValue(0)]
        public int numOfVisit { get; set; }

        public CafeOutlet() {
            numOfVisit = 0;
            numOfLike = 0;
            cafeContactNum = "-";
        }

        public string cafeLat { get; set; }
        public string CafeLong { get; set; }
        
        public string cafeRegion { get; set; }

        [DefaultValue(0)]
        public int numOfLike { get; set; }
    }
}