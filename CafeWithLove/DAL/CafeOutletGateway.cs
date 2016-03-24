using CafeWithLove.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CafeWithLove.DAL
{
    public class CafeOutletGateway : DataGateway<CafeOutlet>
    {
        
        public ICollection<CafeOutlet> model;
        
        public ICollection<CafeOutlet> getOutlet(int cafeId)
        {
            ICollection<CafeOutlet> model = new List<CafeOutlet>();
            
            model = data.Where(p => p.cafeId == cafeId).ToList();
            return model;
        }

        // get all cafes based on array of Scafeoutletid
        public ICollection<CafeOutlet> SelectByIdArray(int[] cafeOutletIds)
        {
            ICollection<CafeOutlet> model = new List<CafeOutlet>();

            model = data.Where(p => cafeOutletIds.Contains(p.cafeOutletId)).ToList();
            return model;
        }

        public ICollection<CafeOutlet> MostVisited(int numOfCafes)
        {
            ICollection<CafeOutlet> model = data.OrderByDescending(x => x.numOfVisit).Take(numOfCafes).ToList();

            //throw new NotImplementedException();

            return model;
        }

        public void UpdateNumOfVisits(CafeOutlet cafe)
        {
            cafe.numOfVisit++;
            db.Entry(cafe).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
