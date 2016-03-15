using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class Search
    {
        public int Id { get; set; }

        public String searchKeyword { get; set; }

        [DefaultValue(1)]
        public int numOfSearches { get; set; }
    }
}