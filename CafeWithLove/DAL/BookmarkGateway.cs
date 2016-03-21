using CafeWithLove.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeWithLove.DAL
{
    public class BookmarkGateway : DataGateway<Bookmark>
    {
        //initialise cafedetails and store into our data within this class
        public BookmarkGateway()
        {
            this.data = db.Set<Bookmark>();
        }

        // check if user has bookmarked this cafe
        public bool IfBookmarked(int cafeOutletId, string userId)
        {
            IEnumerable<Bookmark> model = data.Where(x => x.cafeOutletId.Equals(cafeOutletId) && x.userId.Equals(userId)).Take(1);
            if (model.Any())
                return true;
            else
                return false;
        }

        public Bookmark Delete(int cafeOutletId, string userId)
        {
            IEnumerable<Bookmark> model = data.Where(x => x.cafeOutletId.Equals(cafeOutletId) && x.userId.Equals(userId)).Take(1);
            Bookmark foundBookmark = model.First();

            data.Remove(foundBookmark);
            db.SaveChanges();
            return foundBookmark;

        }
    }
}