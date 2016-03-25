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
        public CafeOpeningHour SelectByCafeOutletId(int outletId)
        {
            return data.Where(p => p.cafeOutletId == outletId).First();
        }

        public void DeleteByOutletId(int outletId)
        {
            CafeOpeningHour obj = data.Where(p => p.cafeOutletId == outletId).First();
            data.Remove(obj);
            db.SaveChanges();
        }
    }
}