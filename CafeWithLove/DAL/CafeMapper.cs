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

        List<CafeViewModel> modelList = new List<CafeViewModel>();

        public ICollection<CafeViewModel> CafeMap(String searchInput)
        {
            IEnumerable<CafeDetail> cafeList = cafeDetailGateway.Search(searchInput);

            MapOutletToCafe(cafeList);

            return modelList;
        }

        public ICollection<CafeViewModel> CafeMapAll()
        {
            IEnumerable<CafeDetail> cafeList = cafeDetailGateway.SelectAll();

            MapOutletToCafe(cafeList);

            return modelList;
        }

        public OutletViewModel CafeOutletMap(int outletID)
        {
            CafeOutlet cafeOutlet = cafeOutletGateway.SelectById(outletID);
            CafeDetail cafeDetail = cafeDetailGateway.SelectById(cafeOutlet.cafeId);
            cafeDetailGateway.UpdateNumOfVisits(cafeDetail);
            OutletViewModel tempmodel = new OutletViewModel();
            tempmodel.CafeDetailVM = cafeDetail;
            tempmodel.CafeOutletVM = cafeOutlet;
            
            return tempmodel;
        }

        // different from the rest
        // gets CafeOutlet before CafeDetails
        public ICollection<CafeViewModel> CafeMapBookmarks(int[] cafeOutletIds)
        {
            ICollection<CafeOutlet> outletList = cafeOutletGateway.SelectByIdArray(cafeOutletIds);        // get list of outlets
            int[] cafeIds = outletList.Select(outlet => outlet.cafeId).Distinct().ToArray();   // get all cafeIds of cafeArray
            
            IEnumerable<CafeDetail> cafeList = cafeDetailGateway.BookmarkedCafes(cafeIds);        // get list of outlets
            
            foreach (var cafe in cafeList)      // loop through all cafes in cafeList
            {
                ICollection<CafeOutlet> outlets = new List<CafeOutlet>();
                outlets = outletList.Where(outlet => outlet.cafeId == cafe.Id).ToList();
                CafeViewModel tempmodel = new CafeViewModel();
                tempmodel.CafeDetailVM = cafe;
                tempmodel.CafeOutletVM = outlets;       // store all outlets into the same cafedetail
                modelList.Add(tempmodel);
            }

            return modelList;
        }

        public ICollection<CafeViewModel> MostVisited(int numOfCafes)
        {
            IEnumerable<CafeDetail> cafeList = cafeDetailGateway.MostVisited(numOfCafes);

            MapOutletToCafe(cafeList);

            return modelList;
        }

        public ICollection<CafeViewModel> CafePFilter(string chosen)
        {
            IEnumerable<CafeDetail> CafeList = cafeDetailGateway.PFilter(chosen);

            foreach (CafeDetail cafe in CafeList)
            {
                ICollection<CafeOutlet> outletList = cafeOutletGateway.getOutlet(cafe.Id);
                CafeViewModel tempmodel = new CafeViewModel();
                tempmodel.CafeDetailVM = cafe;
                tempmodel.CafeOutletVM = outletList;
                modelList.Add(tempmodel);
            }
            return modelList;
        }

        // query CafeOutlet database based on list of cafes from CafeDetails
        // map each CafeOutlet to CafDetails based on cafeId
        public void MapOutletToCafe(IEnumerable<CafeDetail> cafeList)
        {
            int[] cafeIds = cafeList.Select(cafe => cafe.Id).Distinct().ToArray();

            ICollection<CafeOutlet> outletList = cafeOutletGateway.SelectByIdArray(cafeIds);        // get list of outlets

            foreach (CafeDetail cafe in cafeList)      // loop through all cafes in cafeList
            {
                ICollection<CafeOutlet> outlets = new List<CafeOutlet>();
                outlets = outletList.Where(outlet => outlet.cafeId == cafe.Id).ToList();
                CafeViewModel tempmodel = new CafeViewModel();
                tempmodel.CafeDetailVM = cafe;
                tempmodel.CafeOutletVM = outlets;       // store all outlets into the same cafedetail
                modelList.Add(tempmodel);
            }
        }
    }
}