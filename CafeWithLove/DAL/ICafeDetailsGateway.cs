using CafeWithLove.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeWithLove.DAL
{
    interface ICafeDetailsGateway
    {
        IEnumerable<CafeDetail> SelectAll();
        CafeDetail SelectById(int? id);
        void Insert(CafeDetail cafedetail);
        void Update(CafeDetail cafedetail);
        CafeDetail Delete(int? id);
        void save();
    }
}
