using CafeWithLove.Models;
using System.Collections.Generic;
using System.Linq;

namespace CafeWithLove.DAL
{
    public class CafeOutletGateway : DataGateway<CafeOutlet>
    {
        private ICollection<CafeOutlet> temp;
        public ICollection<CafeOutlet> model; 

        public ICollection<CafeOutlet> Populate(IEnumerable<CafeDetail> result)
        {
            ICollection<CafeOutlet> model = new List<CafeOutlet>();
            foreach (CafeDetail cafe in result)
            {
                temp=data.Where(p => p.cafeId == cafe.Id).ToList();

                foreach(CafeOutlet t in temp)
                {
                    model.Add(t);
                }
            };

            return model;
        }
    }
}
