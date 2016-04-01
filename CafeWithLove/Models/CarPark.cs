using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeWithLove.Models
{
    public class CarPark
    {
        [Required]
        public int id { get; set; }
        public string carparkNo { get; set; }
        public string address { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string carparkType { get; set; }
        public string typeOfparking { get; set; }
        public string shortTermparking { get; set; }
        public string freeParking { get; set; }
        public string nightParking { get; set; }
        public string parkAndrideScheme { get; set; }
        public string adhocParking { get; set; }
    }
}