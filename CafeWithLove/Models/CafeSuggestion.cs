using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class CafeSuggestion
    {
        public int Id { get; set; }

        public string userId { get; set; }

        [DisplayName("Cafe Name")]
        [Required]
        public string cafeName { get; set; }

        [DisplayName("Cafe Website")]
        public string cafeWebsite { get; set; }

        [DisplayName("Cafe Description")]
        [MaxLength(255)]
        public string cafeDesc { get; set; }

        public string status { get; set; }

        public CafeSuggestion()
        {
            this.status = "Pending";
        }
    }
}