using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeWithLove.DAL
{
    //interface that can accept any model class
    interface IDataGateway<T> where T : class
    {
        IEnumerable<T> SelectAll();
        T SelectById(int? id);
        void Insert(T obj);
        void Update(T obj);
        T Delete(int? id);
        void Save();
    }
}