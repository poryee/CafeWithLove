using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class Bookmark
    {
        [Key]
        [Column(Order = 1)]
        public int cafeOutletId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string userId { get; set; }

        public Bookmark() { }

        public Bookmark(int newID, string userId1)
        {
            this.cafeOutletId = newID;
            this.userId = userId1;
        }
    }
}