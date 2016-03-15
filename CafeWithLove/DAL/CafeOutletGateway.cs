using CafeWithLove.Models;
using System.Collections.Generic;
using System.Linq;

namespace CafeWithLove.DAL
{
    public class CafeOutletGateway : DataGateway<CafeOutlet>
    {
        
        public ICollection<CafeOutlet> model; 

        public ICollection<CafeOutlet> getOutlet(CafeDetail cafe)
        {
            ICollection<CafeOutlet> model = new List<CafeOutlet>();
            
            model = data.Where(p => p.cafeId == cafe.Id).ToList();
            return model;
        }
    }
}
