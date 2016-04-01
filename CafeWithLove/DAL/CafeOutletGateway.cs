using CafeWithLove.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace CafeWithLove.DAL
{
    public class CafeOutletGateway : DataGateway<CafeOutlet>
    {
        
        public ICollection<CafeOutlet> model;
        
        // list of outlets to one cafe
        public ICollection<CafeOutlet> getOutlet(int cafeId)
        {
            ICollection<CafeOutlet> model = new List<CafeOutlet>();
            
            model = data.Where(p => p.cafeId == cafeId).ToList();
            return model;
        }

        // get all outlets based on array of cafeIds
        public ICollection<CafeOutlet> SelectByIdArray(int[] cafeIds)
        {
            ICollection<CafeOutlet> model = new List<CafeOutlet>();

            model = data.Where(p => cafeIds.Contains(p.cafeId)).ToList();
            return model;
        }

        // get top visited outlets
        public ICollection<CafeOutlet> MostVisited(int numOfCafes)
        {
            ICollection<CafeOutlet> model = data.OrderByDescending(x => x.numOfVisit).Take(numOfCafes).ToList();

            //throw new NotImplementedException();

            return model;
        }

        // update num of visits to outlet
        public void UpdateNumOfVisits(CafeOutlet cafe)
        {
            cafe.numOfVisit++;
            db.Entry(cafe).State = EntityState.Modified;
            db.SaveChanges();
        }

        // insert one outlet and return its id
        public int InsertReturnId(CafeOutlet cafe)
        {
            data.Add(cafe);
            db.SaveChanges();
            return cafe.cafeOutletId;
        }

        // delete outlet based on cafe id
        public ICollection<CafeOutlet> DeleteByCafeId(int cafeId)
        {
            // return a collection of cafeoutlets instead of one cafeoutlet in datagateway
            ICollection<CafeOutlet> model = data.RemoveRange(data.Where(x => x.cafeId.Equals(cafeId))).ToList();
            db.SaveChanges();
            return model;
        }

        //returns matching region
        public ICollection<CafeOutlet> regionFilter(string chosen)
        {
            ICollection<CafeOutlet> model = new List<CafeOutlet>();

            model = data.Where(p => p.cafeRegion.Equals(chosen)).ToList();
            return model;
        }
    }
}
