using CafeWithLove.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeWithLove.Controllers
{
    public class SearchController : Controller
    {
        private SearchGateway searchGateway = new SearchGateway();

        // GET: Search
        public ActionResult _PopularSearchPartial()
        {
            return PartialView(searchGateway.GetPopularSearch());
        }
    }
}