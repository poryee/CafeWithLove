using CafeWithLove.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CafeWithLove.DAL
{
    //our cafedetailgateway 
    public class CafeDetailGateway : DataGateway<CafeDetail>
    {
        //initialise cafedetails and store into our data within this class
        public CafeDetailGateway()
        {
            this.data = db.Set<CafeDetail>();
        }

        //public void save()
        //{
        //    db.SaveChanges();
        //    //throw new NotImplementedException();
        //}

        //public CafeDetail Delete(int? id)
        //{
        //    CafeDetail obj = data.Find(id);
        //    data.Remove(obj);
        //    db.SaveChanges();
        //    return obj;
        //    //throw new NotImplementedException();

        //}

        //public void Insert(CafeDetail obj)
        //{
        //    data.Add(obj);
        //    save();
        //    //throw new NotImplementedException();
        //}

        //public IEnumerable<CafeDetail> SelectAll()
        //{
        //    IEnumerable<CafeDetail> model = data.ToList();

        //    return model;
        //    //throw new NotImplementedException();
        //}

        //public CafeDetail SelectById(int? id)
        //{
        //    CafeDetail obj = data.Find(id);
        //    return obj;
        //    //throw new NotImplementedException();
        //}

        //public void Update(CafeDetail obj)
        //{
        //    db.Entry(obj).State = EntityState.Modified;
        //    save();
        //    //throw new NotImplementedException();
        //}

        public IEnumerable<CafeDetail> Search(String input)
        {
            input = input.ToUpper();
            IEnumerable<CafeDetail> model = db.CafeDetails.Where(
                                            x => x.cafeName.ToUpper().Contains(input) ||
                                            x.cafeCategory.ToUpper().Contains(input) ||
                                            input == null) .ToList();
            //throw new NotImplementedException();

            return model;
        }

        public void Dispose()
        {

            db.Dispose();
            //throw new NotImplementedException();
        }

    }
}