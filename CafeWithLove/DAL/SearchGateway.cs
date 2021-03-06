﻿using CafeWithLove.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CafeWithLove.DAL
{
    public class SearchGateway : DataGateway<Search>
    {
        //initialise search and store into our data within this class
        public SearchGateway()
        {
            this.data = db.Set<Search>();
        }

        public virtual void Insert(String userInputKeyword)
        {
            if (!new CafeDetailGateway().ValidNameOrCat(userInputKeyword))
                return;

            // caps first letter of each word
            userInputKeyword = Regex.Replace(userInputKeyword, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            Search obj = SelectBySearchKeyword(userInputKeyword);
           
            // not searched before in search database, insert new row
            if (obj == null)
            {
                obj = new Search();
                obj.searchKeyword = userInputKeyword;
                obj.numOfSearches = 1;
                data.Add(obj);
            }
            else    // searched before, update row
                Update(obj);
            
            db.SaveChanges();
        }

        public virtual new void Update(Search obj)
        {
            obj.numOfSearches++;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Search SelectBySearchKeyword(String userInputKeyword)
        {
            IEnumerable<Search> model = data.Where(x => x.searchKeyword.Contains(userInputKeyword)).Take(1);
            if (model.Any())
                return model.First();
            else
                return null;
        }

        public IEnumerable<Search> GetPopularSearch()
        {
            IEnumerable<Search> model = data.OrderByDescending(x => x.numOfSearches).Take(10);
            return model;
        }
    }
}