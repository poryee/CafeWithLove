using CafeWithLove.Models;
using System.Collections.Generic;
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
    }
}
