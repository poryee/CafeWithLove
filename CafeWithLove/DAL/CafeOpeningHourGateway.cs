using CafeWithLove.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeWithLove.DAL
{
    public class CafeOpeningHourGateway : DataGateway<CafeOpeningHour>
    {
        public CafeOpeningHour SelectByCafeOutletId(int outletId)
        {
            return data.Where(p => p.cafeOutletId == outletId).First();
        }
    }
}