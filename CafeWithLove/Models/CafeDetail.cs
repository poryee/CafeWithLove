using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class CafeDetail
    {
        public int Id { get; set; }
        
        public string cafeName { get; set; }

        public string cafeDesc { get; set; }

        public string cafeLogo { get; set; }

        [DisplayName("Price")]
        public int cafePrice { get; set; }

        [DisplayName("Rating")]
        public decimal cafeRating { get; set; }

        public string cafeCategory { get; set; }

        [DisplayName("Website")]
        public string cafeWebsite { get; set; }

        public int numOfVisit { get; set; }


    }
}