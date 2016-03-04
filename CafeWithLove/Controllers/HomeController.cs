using CafeWithLove.DAL;
using CafeWithLove.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeWithLove.Controllers
{
    public class HomeController : Controller
    {

        private CafeDetailGateway cafeDetailGateway = new CafeDetailGateway();
        private CafeOutletGateway cafeOutletGateway = new CafeOutletGateway();

        public ActionResult Index()
        {


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CafeDetails()
        {
            ViewBag.Message = "Cafe Details";

            return View();
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Your search page.";

           return View(cafeDetailGateway.Search(null));
        }

        [HttpPost]
        public ActionResult Search(string searchInput)
        {
            ViewBag.Message = "Your search page.";
            ViewModel mymodel = new ViewModel();
            IEnumerable<CafeDetail> result= cafeDetailGateway.Search(searchInput);
            mymodel.CafeDetailVM = result;


            mymodel.CafeOutletVM = cafeOutletGateway.Populate(result);
            return View(mymodel);
        }
    }
}