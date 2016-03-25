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

        // get all cafes based on array of cafeIds
        public ICollection<CafeOutlet> SelectByIdArray(int[] cafeIds)
        {
            ICollection<CafeOutlet> model = new List<CafeOutlet>();

            model = data.Where(p => cafeIds.Contains(p.cafeId)).ToList();
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

        public int InsertReturnId(CafeOutlet cafe)
        {
            data.Add(cafe);
            db.SaveChanges();
            return cafe.cafeOutletId;
        }
    }
}
