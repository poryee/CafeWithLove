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
using Microsoft.AspNet.Identity;

namespace CafeWithLove.Controllers
{
    public class CafeDetailsController : Controller
    {
        private CafeDetailGateway cafeDetailGateway = new CafeDetailGateway();
        private CafeOutletGateway cafeOutletGateway = new CafeOutletGateway();
        private BookmarkGateway bookmarkGateway = new BookmarkGateway();
        private SearchGateway searchGateway = new SearchGateway();

        private CafeMapper cafeMapper = new CafeMapper();

        private CafeWithLoveContext db = new CafeWithLoveContext();
        
        // GET: CafeDetails
        public ActionResult Index()
        {
            ViewBag.Heading = "Browse all cafes";

            ICollection<CafeViewModel> mymodel = cafeMapper.CafeMapAll();

            return View(mymodel);
        }

        // GET: CafeFilter by Price
        public ActionResult PFilter(string chosen)
        {
            if (chosen == null)                         // no filter chosen, redirect to index
                return RedirectToAction("Index");

            ViewBag.Heading = "Browse cafes with price (" + new string('$', Int32.Parse(chosen)) + ")";

            ICollection<CafeViewModel> mymodel = null;

            mymodel = cafeMapper.CafePFilter(chosen);
            
            return View("Index", mymodel);
        }

        // GET: CafeFilter by Region
        public ActionResult RFilter()
        {
            ICollection<CafeViewModel> mymodel = cafeMapper.CafeMapAll();

            return View("Index", mymodel);
        }

        // GET: Featured Cafes
        // MUST BE CHANGED
        public ActionResult _FeaturedCafes()
        {
            //ICollection<CafeViewModel> mymodel = cafeMapper.MostVisited();
            IEnumerable<CafeDetail> mymodel = cafeDetailGateway.MostVisited(4);

            return PartialView(mymodel);
        }

        // GET: CafeDetails
        // MUST BE CHANGED
        public ActionResult TopTen()
        {
            ViewBag.Heading = "Top 10 Cafes";

            ICollection<CafeViewModel> mymodel = cafeMapper.MostVisited(10);

            return View("Index", mymodel);
        }

        // GET: CafeDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                bool bookmarked = bookmarkGateway.IfBookmarked((int)id, userId);
                @ViewBag.Bookmarked = bookmarked;

                if (bookmarked)
                {
                    @ViewBag.BookmarkClass = "btn-yellow-inverse";
                }
                else
                {
                    @ViewBag.BookmarkClass = "btn-yellow";
                }
            }

            return View(cafeMapper.CafeOutletMap((int)id));
        }

        [Authorize]             // only logged in users can view this page
        public ActionResult Bookmark(int? newID, bool bookmarked)
        {
            string userId = User.Identity.GetUserId();
            Bookmark newBookmark = new Bookmark((int)newID, userId);

            if(bookmarked)
                bookmarkGateway.Delete((int)newID, userId);            // should be delete
            else
                bookmarkGateway.Insert(newBookmark);

            return RedirectToAction("Details", "CafeDetails", new { id = newID });
        }

        //GET: CafeDetails/Bookmarks
        [Authorize]             // only logged in users can view this page
        public ActionResult Bookmarks()
        {
            ViewBag.Heading = "Bookmarked Cafes";

            string userId = User.Identity.GetUserId();
            IEnumerable<Int32> bookmarkCafes = bookmarkGateway.GetBookmarks(userId);        // get all bookmarked cafes cafeoutletid
            int[] cafeOutletIds = bookmarkCafes.Cast<int>().ToArray();              // convert to array
            ICollection<CafeViewModel> mymodel = cafeMapper.CafeMapBookmarks(cafeOutletIds);

            return View("Index", mymodel);
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
            if (searchInput == null || searchInput.Trim().Equals(""))
                return RedirectToAction("Index");           // no search input, just show index page

            ViewBag.Heading = "Browse cafes with \"" + searchInput + "\"";
            
            ICollection<CafeViewModel> mymodel = cafeMapper.CafeMap(searchInput);
            
            // results found, add/update search database
            if (mymodel.Any())
            {
                searchGateway.Insert(searchInput);
            }
            
            return View("Index", mymodel);
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
