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
        
        [DisplayName("Cafe Name")]
        [Required]
        public string cafeName { get; set; }

        [DisplayName("Cafe Description")]
        [MaxLength(255)]
        [Required]
        public string cafeDesc { get; set; }

        [DisplayName("Cafe Logo")]
        [Required]
        public string cafeLogo { get; set; }

        [DisplayName("Price")]
        [Range(1, 5)]
        [Required]
        public int cafePrice { get; set; }
        
        [DisplayName("Rating")]
        [Range(1, 5)]
        [Required]
        public decimal cafeRating { get; set; }

        [DisplayName("Category")]
        [Required]
        public string cafeCategory { get; set; }

        [DisplayName("Website")]
        [Required]
        public string cafeWebsite { get; set; }
    }
}