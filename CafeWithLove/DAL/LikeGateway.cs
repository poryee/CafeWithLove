using CafeWithLove.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeWithLove.DAL
{
    public class LikeGateway : DataGateway<Like>
    {
        //initialise cafedetails and store into our data within this class
        public LikeGateway()
        {
            this.data = db.Set<Like>();
        }

        // check if user has bookmarked this cafe
        public bool IfLiked(int cafeOutletId, string userId)
        {
            IEnumerable<Like> model = data.Where(x => x.cafeOutletId.Equals(cafeOutletId) && x.userId.Equals(userId)).Take(1);
            if (model.Any())
                return true;
            else
                return false;
        }

        public Like Delete(int cafeOutletId, string userId)
        {
            IEnumerable<Like> model = data.Where(x => x.cafeOutletId.Equals(cafeOutletId) && x.userId.Equals(userId)).Take(1);
            Like foundLike = model.First();

            data.Remove(foundLike);
            db.SaveChanges();
            return foundLike;
        }

        public IEnumerable<Int32> GetLikes(string userId)
        {
            IEnumerable<Int32> model = data.Where(x => x.userId.Equals(userId)).Select(x => x.cafeOutletId);

            return model;
        }

        //public var GetNumOfLikes()
        //{
        //    var numOfLikes = data.GroupBy(x => x.cafeOutletId).Select(g => new { g.Key, Count = g.Count() });
        //    return numOfLikes;
        //}
    }
}