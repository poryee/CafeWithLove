using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CafeWithLove.DAL;
using CafeWithLove.Models;

namespace CafeWithLove.Controllers
{
    public class CafeDetailsController : Controller
    {
        private CafeDetailGateway cafeDetailGateway = new CafeDetailGateway();
        private CafeOutletGateway cafeOutletGateway = new CafeOutletGateway();
        private SearchGateway searchGateway = new SearchGateway();

        private CafeMapper cafeMapper = new CafeMapper();

        private CafeWithLoveContext db = new CafeWithLoveContext();

        // GET: CafeDetails
        public ActionResult Index()
        {

           ICollection<CafeViewModel> mymodel = cafeMapper.CafeMapAll();

            return View(mymodel);
        }

        // GET: CafeFilter by Price
        public ActionResult PFilter(string chosen)
        {
            ICollection<CafeViewModel> mymodel = null;

            if (chosen == null)
            {
                mymodel = cafeMapper.CafeMapAll();
            }
            else
            {
                mymodel = cafeMapper.CafePFilter(chosen);
            }


            return View("Index", mymodel);
        }

        // GET: CafeFilter by Region
        public ActionResult RFilter()
        {

            ICollection<CafeViewModel> mymodel = cafeMapper.CafeMapAll();

            return View("Index", mymodel);
        }

        // GET: CafeDetails
        public ActionResult _FeaturedCafes()
        {
            ICollection<CafeViewModel> mymodel = cafeMapper.MostVisited();

            return PartialView(mymodel);
        }

        // GET: CafeDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            return View(cafeMapper.CafeOutletMap((int)id));
        }

        // GET: CafeDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CafeDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cafeName,cafeDesc,cafeLogo,cafePrice,cafeRating,cafeCategory,cafeWebsite,numOfVisit")] CafeDetail cafeDetails)
        {
            if (ModelState.IsValid)
            {
                cafeDetailGateway.Insert(cafeDetails);
                return RedirectToAction("Index");
            }

            return View(cafeDetails);
        }

        // GET: CafeDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(cafeDetailGateway.SelectById(id));
        }

        // POST: CafeDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cafeName,cafeDesc,cafeLogo,cafePrice,cafeRating,cafeCategory,cafeWebsite,numOfVisit")] CafeDetail cafeDetails)
        {
            if (ModelState.IsValid)
            {
                cafeDetailGateway.Update(cafeDetails);
                return RedirectToAction("Index");
            }
            return View(cafeDetails);
        }

        // GET: CafeDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
          
            return View(cafeDetailGateway.SelectById(id));
        }

        // POST: CafeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cafeDetailGateway.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(string searchInput)
        {
            ViewBag.Message = "Your search page.";


            ICollection<CafeViewModel> mymodel = cafeMapper.CafeMap(searchInput);


            // results found, add/update search database
            if (mymodel.Any())
            {
                searchGateway.Insert(searchInput);
            }

           
            return View(mymodel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
