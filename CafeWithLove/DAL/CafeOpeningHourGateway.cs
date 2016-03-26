using CafeWithLove.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeWithLove.DAL
{
    public class CafeOpeningHourGateway : DataGateway<CafeOpeningHour>
    {
        // assuming one row of opening hour for each cafe
        public CafeOpeningHour SelectByOutletId(int outletId)
        {
            return data.Where(p => p.cafeOutletId == outletId).First();
        }

        public ICollection<CafeOpeningHour> SelectByOutletIds(int[] outletIds)
        {
            ICollection<CafeOpeningHour> model = new List<CafeOpeningHour>();

            model = data.Where(p => outletIds.Contains(p.cafeOutletId)).ToList();
            return model;

        }

        public void DeleteByOutletId(int outletId)
        {
            CafeOpeningHour obj = data.Where(p => p.cafeOutletId == outletId).First();
            data.Remove(obj);
            db.SaveChanges();
        }

        public ICollection<CafeOpeningHour> DeleteByOutletIds(int[] outletIds)
        {
            ICollection<CafeOpeningHour> model = data.RemoveRange(data.Where(p => outletIds.Contains(p.cafeOutletId))).ToList();
            db.SaveChanges();

            return model;
        }
    }
}