﻿using CafeWithLove.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CafeWithLove.DAL
{
    public class CafeMapper
    {
        private CafeDetailGateway cafeDetailGateway = new CafeDetailGateway();
        private CafeOutletGateway cafeOutletGateway = new CafeOutletGateway();
        //public List<CafeViewModel> temp;
        List<CafeViewModel> modelList = new List<CafeViewModel>();

        public ICollection<CafeViewModel> CafeMap(String searchInput)
        {
            
            IEnumerable<CafeDetail> CafeList = cafeDetailGateway.Search(searchInput);
            
            foreach (CafeDetail cafe in CafeList)
            {
                ICollection<CafeOutlet> outletList = cafeOutletGateway.getOutlet(cafe);
                CafeViewModel tempmodel = new CafeViewModel();
                tempmodel.CafeDetailVM = cafe;
                tempmodel.CafeOutletVM = outletList;
                //Debug.WriteLine("Send to debug output.");
                modelList.Add(tempmodel);
                //Debug.WriteLine("Send to debug output.");
            }


      
            return modelList;
        }

        public ICollection<CafeViewModel> CafeMapAll()
        {
            
            IEnumerable<CafeDetail> CafeList = cafeDetailGateway.SelectAll();

            foreach (CafeDetail cafe in CafeList)
            {
                ICollection<CafeOutlet> outletList = cafeOutletGateway.getOutlet(cafe);
                CafeViewModel tempmodel = new CafeViewModel();
                tempmodel.CafeDetailVM = cafe;
                tempmodel.CafeOutletVM = outletList;
                modelList.Add(tempmodel);
            }



            return modelList;
        }

    }
}