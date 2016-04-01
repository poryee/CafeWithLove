using CafeWithLove.Models;
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
        private CafeOpeningHourGateway cafeOpeningHourGateway = new CafeOpeningHourGateway();
        private LikeGateway likeGateway = new LikeGateway();

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

        // select one CAFE and all its outlets + opening hours to its outlets
        public CafeViewModel SelectOneCafe(int cafeId)
        {
            CafeViewModel cvm = new CafeViewModel();
            cvm.CafeDetailVM = cafeDetailGateway.SelectById(cafeId);
            cvm.CafeOutletVM = cafeOutletGateway.getOutlet(cafeId);

            int[] outletIds = cvm.CafeOutletVM.Select(outlet => outlet.cafeOutletId).Distinct().ToArray();
            cvm.CafeOpeningHourVM = cafeOpeningHourGateway.SelectByOutletIds(outletIds);

            return cvm;
        }

        // select one OUTLET + its opening hours + its details from CafeDetail
        // gets CafeOutlet before CafeDetails
        public OutletViewModel CafeOutletMap(int outletID)
        {
            CafeOutlet cafeOutlet = cafeOutletGateway.SelectById(outletID);
            CafeDetail cafeDetail = cafeDetailGateway.SelectById(cafeOutlet.cafeId);
            CafeOpeningHour cafeOpeningHour = cafeOpeningHourGateway.SelectByOutletId(outletID);
            cafeOutletGateway.UpdateNumOfVisits(cafeOutlet);
            OutletViewModel tempmodel = new OutletViewModel();
            tempmodel.CafeDetailVM = cafeDetail;
            tempmodel.CafeOutletVM = cafeOutlet;
            tempmodel.CafeOpeningHourVM = cafeOpeningHour;

            return tempmodel;
        }

        

        // gets CafeOutlet before CafeDetails
        // gets all bookmarked/liked OUTLETs
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

        // gets CafeOutlet before CafeDetails
        // gets most visited OUTLETs
        public ICollection<OutletViewModel> MostVisited(int numOfCafes)
        {
            //IEnumerable<CafeDetail> cafeList = cafeDetailGateway.MostVisited(numOfCafes);

            //MapOutletToCafe(cafeList);

            //return modelList;

            ICollection<CafeOutlet> outletList = cafeOutletGateway.MostVisited(numOfCafes);        // get list of outlets
            int[] cafeIds = outletList.Select(outlet => outlet.cafeId).Distinct().ToArray();   // get all cafeIds of cafeArray

            IEnumerable<CafeDetail> cafeList = cafeDetailGateway.BookmarkedCafes(cafeIds);        // get list of outlets

            List<OutletViewModel> outletModelList = new List<OutletViewModel>();

            foreach (var outlet in outletList)      // loop through all cafes in cafeList
            {
                ICollection<CafeOutlet> outlets = new List<CafeOutlet>();
                OutletViewModel tempmodel = new OutletViewModel();

                for (int i = 0; i < cafeList.Count(); i++)
                {
                    if (cafeList.ElementAt(i).Id.Equals(outlet.cafeId))
                    {
                        tempmodel.CafeDetailVM = cafeList.ElementAt(i);
                        break;
                    }
                }
                tempmodel.CafeOutletVM = outlet;       // store all outlets into the same cafedetail
                outletModelList.Add(tempmodel);
            }

            return outletModelList;
        }


        //filter by price
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

        //filter by category
        public ICollection<CafeViewModel> CafeCFilter(string chosen)
        {
            IEnumerable<CafeDetail> CafeList = cafeDetailGateway.CFilter(chosen);

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


        //filter by region
        public ICollection<CafeViewModel> CafeRFilter(string chosen)
        {
            ICollection<CafeOutlet> outletList = cafeOutletGateway.regionFilter(chosen);        // get list of outlets
            int[] cafeIds = outletList.Select(outlet => outlet.cafeId).Distinct().ToArray();   // get all cafeIds of cafeArray

            IEnumerable<CafeDetail> cafeList = cafeDetailGateway.BookmarkedCafes(cafeIds);        // get list of outlets

            foreach (CafeDetail cafe in cafeList)
            {
                ICollection<CafeOutlet> templist = cafeOutletGateway.getOutlet(cafe.Id);
                CafeViewModel tempmodel = new CafeViewModel();
                tempmodel.CafeDetailVM = cafe;
                tempmodel.CafeOutletVM = templist;
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

        // insert one OUTLET
        public void InsertOutlet(OutletViewModel ovm)
        {
            int cafeOutletId = cafeOutletGateway.InsertReturnId(ovm.CafeOutletVM);
            ovm.CafeOpeningHourVM.cafeOutletId = cafeOutletId;
            cafeOpeningHourGateway.Insert(ovm.CafeOpeningHourVM);
        }

        // update one OUTLET
        public void UpdateOutlet(OutletViewModel ovm)
        {
            cafeOutletGateway.Update(ovm.CafeOutletVM);
            cafeOpeningHourGateway.Update(ovm.CafeOpeningHourVM);
        }

        // delete one OUTLET
        public void DeleteOutlet(int outletId)
        {
            cafeOutletGateway.Delete(outletId);
            cafeOpeningHourGateway.DeleteByOutletId(outletId);
        }

        // delete one CAFE
        public void DeleteCafe(int cafeId)
        {
            cafeDetailGateway.Delete(cafeId);
            ICollection<CafeOutlet> deletedOutletList = cafeOutletGateway.DeleteByCafeId(cafeId);
            int[] deletedOutletIds = deletedOutletList.Select(outlet => outlet.cafeOutletId).Distinct().ToArray();
            cafeOpeningHourGateway.DeleteByOutletIds(deletedOutletIds);
        }

        public void doLike(int outletId, string userId)
        {
            likeGateway.Insert(new Like(outletId, userId));
            cafeOutletGateway.addLike(outletId);
        }

        public void removeLike(int outletId)
        {
            likeGateway.Delete(outletId);
            cafeOutletGateway.removeLike(outletId);
        }
    }
}