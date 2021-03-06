﻿using CafeWithLove.Models;
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

        public ICollection<CafeDetail> Random()
        {
            List<CafeDetail> mymodel = new List<CafeDetail>();
            int ran;
            //randomly select 5 
            Random rnd = new Random();
            IEnumerable<CafeDetail> model = db.CafeDetails.GroupBy(c => c.cafeCategory).Select(grp => grp.FirstOrDefault()).Distinct().ToList();
            int number = model.Count();
            //throw new NotImplementedException();
            for (int i = 0; i < 5; i++)
            {
                do
                {
                    ran = rnd.Next(number);
                    if (!mymodel.Contains(model.ElementAt(ran)))
                    {
                        mymodel.Add(model.ElementAt(ran));
                        break;
                    }
                } while (true);
            }
            //mymodel.GroupBy(s => s.cafeCategory).Select(grp => grp.FirstOrDefault());

            return mymodel;
        }




        public IEnumerable<CafeDetail> Search(String input)
        {
            input = input.ToUpper();
            IEnumerable<CafeDetail> model = db.CafeDetails.Where(
                                            x => x.cafeName.ToUpper().Contains(input) ||
                                            x.cafeCategory.ToUpper().Contains(input) ||
                                            input == null) .ToList();
           

            return model;
        }

       

        public IEnumerable<CafeDetail> PFilter(string chosen)
        {

            int price = int.Parse(chosen);
            IEnumerable<CafeDetail> model = db.CafeDetails.Where(
                                            x => x.cafePrice== price).ToList();
            //throw new NotImplementedException();

            return model;
        }

        public IEnumerable<CafeDetail> CFilter(string chosen)
        {

            
            IEnumerable<CafeDetail> model = db.CafeDetails.Where(
                                            x => x.cafeCategory.Equals(chosen)).ToList();
            //throw new NotImplementedException();

            return model;
        }

        public bool ValidNameOrCat(String input)
        {
            input = input.ToUpper();
            IEnumerable<CafeDetail> model = db.CafeDetails.Where(
                                            x => x.cafeName.ToUpper().Equals(input) ||
                                            x.cafeCategory.ToUpper().Equals(input) ||
                                            input == null).Take(1);
            //throw new NotImplementedException();
            if (model.Any())
                return true;
            return false;
        }
        // get all cafes based on array of cafeid
        public IEnumerable<CafeDetail> BookmarkedCafes(int[] cafeId)
        {
            IEnumerable<CafeDetail> model = data.Where(x => cafeId.Contains(x.Id)).ToList();    // get all cafe with Ids present in cafeId
            
            return model;
        }

        public void Dispose()
        {

            db.Dispose();
            //throw new NotImplementedException();
        }

        
    }
}