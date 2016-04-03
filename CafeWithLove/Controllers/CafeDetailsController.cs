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
        private LikeGateway likeGateway = new LikeGateway();
        private SearchGateway searchGateway = new SearchGateway();
        private CafeSuggestionGateway cafeSuggestionGateway = new CafeSuggestionGateway();
        private CarParkGateway carparkGateway = new CarParkGateway();
        private CafeMapper cafeMapper = new CafeMapper();
        
        // GET: CafeDetails
        public ActionResult Index()
        {
            ViewBag.Heading = "Browse all cafes";

            ICollection<CafeViewModel> mymodel = cafeMapper.CafeMapAll();

            return View(mymodel);
        }

        // GET: ViewAllCafes for admin role
        [Authorize(Roles = "Admin")]
        public ActionResult ManageCafes()
        {
            ViewBag.Heading = "Manage Cafes";

            ICollection<CafeViewModel> mymodel = cafeMapper.CafeMapAll();

            return View(mymodel);
        }


        // GET: CafeFilter by Region
        public ActionResult RFilter(string chosen)
        {
            if (chosen == null)                         // no filter chosen, redirect to index
                return RedirectToAction("Index");

            ViewBag.Heading = "Browse cafes with Region (" + chosen + ")";

            ICollection<CafeViewModel> mymodel = null;

            mymodel = cafeMapper.CafeRFilter(chosen);

            return View("Index", mymodel);
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

        // GET: CafeFilter by Category
        public ActionResult CFilter(string chosen)
        {
            if (chosen == null)                         // no filter chosen, redirect to index
                return RedirectToAction("Index");

            ViewBag.Heading = "Browse cafes with Category (" + chosen + ")";

            ICollection<CafeViewModel> mymodel = null;

            mymodel = cafeMapper.CafeCFilter(chosen);

            return View("Index", mymodel);
        }

        
        // GET: CafeFilter by Region
        public ActionResult _RandomCategory()
        {

            IEnumerable<CafeDetail> mymodel = cafeDetailGateway.Random();

            return PartialView(mymodel);
        }

        // GET: Featured Outlets
        // OUTLETs
        public ActionResult _FeaturedCafes()
        {
            ICollection<OutletViewModel> mymodel = cafeMapper.MostVisited(4);
            //IEnumerable<CafeDetail> mymodel = cafeMapper.MostVisited(4);

            return PartialView(mymodel);
        }

        // GET: CafeDetails
        // OUTLETs
        public ActionResult TopTen()
        {
            ViewBag.Heading = "Top 10 Cafes";

            ICollection<OutletViewModel> mymodel = cafeMapper.MostVisited(10);

            return View(mymodel);
        }

        // GET: CafeDetails/Details/5
        // OUTLET
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
                bool liked = likeGateway.IfLiked((int)id, userId);

                @ViewBag.Bookmarked = bookmarked;
                @ViewBag.Liked = liked;

                if (bookmarked)
                {
                    @ViewBag.BookmarkClass = "btn btn-yellow-inverse btn-lg";
                }
                else
                {
                    @ViewBag.BookmarkClass = "btn btn-yellow btn-lg";
                }

                if (liked)
                {
                    @ViewBag.LikeClass = "btn btn-red-inverse btn-lg";
                }
                else
                {
                    @ViewBag.LikeClass = "btn btn-red btn-lg";
                }
            }
            
            return View(cafeMapper.CafeOutletMap((int)id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DetailsCafe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CafeViewModel cvm = cafeMapper.SelectOneCafe((int)id);
            int outletNum = cvm.CafeOutletVM.Count;
            if (outletNum < 2)
                @ViewBag.outletClass = "col-md-offset-4 col-md-4 center-block";
            else if (outletNum == 2)
                @ViewBag.outletClass = "col-md-offset-1 col-md-5 center-block";
            else
                @ViewBag.outletClass = "col-md-4 center-block";
            return View(cvm);
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

        [Authorize]             // only logged in users can view this page
        public ActionResult Like(int? newID, bool liked)
        {
            string userId = User.Identity.GetUserId();

            if (liked)
                cafeMapper.removeLike((int)newID);
            else
                cafeMapper.doLike((int)newID, userId);            // should be delete

            return RedirectToAction("Details", "CafeDetails", new { id = newID });
        }

        //GET: Bookmarked OUTLETs
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

        //GET: Liked OUTLETs
        [Authorize]             // only logged in users can view this page
        public ActionResult Likes()
        {
            ViewBag.Heading = "Liked Cafes";

            string userId = User.Identity.GetUserId();
            IEnumerable<Int32> likeCafes = likeGateway.GetLikes(userId);        // get all bookmarked cafes cafeoutletid
            int[] cafeOutletIds = likeCafes.Cast<int>().ToArray();              // convert to array
            ICollection<CafeViewModel> mymodel = cafeMapper.CafeMapBookmarks(cafeOutletIds);

            return View("Index", mymodel);
        }

        // GET: CafeDetails/Create
        // CAFE
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            CafeDetail cafeDetail = new CafeDetail();
            cafeDetail.cafeName = Request.Params["cafeName"];
            cafeDetail.cafeDesc = Request.Params["cafeDesc"];
            cafeDetail.cafeWebsite = Request.Params["cafeWebsite"];
            if(Request.Params["cafeSuggestionId"] == null)
                @ViewBag.CafeSuggestionId = "-1";               // not linked from cafe suggestions
            else
                @ViewBag.CafeSuggestionId = Request.Params["cafeSuggestionId"]; // linked from cafe suggestions
            return View(cafeDetail);
        }

        // POST: CafeDetails/Create
        // CAFE
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cafeName,cafeDesc,cafeLogo,cafePrice,cafeRating,cafeCategory,cafeWebsite")] CafeDetail cafeDetails, int cafeSuggestionId,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Images/" + file.FileName);
                file.SaveAs(path);

                cafeDetails.cafeLogo = file.FileName;

                cafeDetailGateway.Insert(cafeDetails);
                if (cafeSuggestionId != -1)
                    cafeSuggestionGateway.Delete(cafeSuggestionId);
                return RedirectToAction("CreateOutlet", new { cafeId = cafeDetails.Id });
            }

            return View(cafeDetails);
        }

        // GET: CafeDetails/CreateOutlet
        // OUTLET
        [Authorize(Roles = "Admin")]
        public ActionResult CreateOutlet(int cafeId)
        {
            OutletViewModel ovm = new OutletViewModel();
            CafeOutlet co = new CafeOutlet();
            ovm.CafeOutletVM = co;
            ovm.CafeOutletVM.cafeId = cafeId;
            return View(ovm);
        }

        // POST: CafeDetails/CreateOutlet
        // OUTLET
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOutlet(OutletViewModel ovm)
        {
            if (ModelState.IsValid)
            {
                cafeMapper.InsertOutlet(ovm);
                return RedirectToAction("ManageCafes");
            }
            
            return View(ovm);
        }

        // GET: CafeDetails/Edit/5
        // CAFE
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(cafeDetailGateway.SelectById(id));
        }

        // POST: CafeDetails/Edit/5
        // CAFE
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cafeName,cafeDesc,cafeLogo,cafePrice,cafeRating,cafeCategory,cafeWebsite,numOfVisit")] CafeDetail cafeDetails, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Images/" + file.FileName);
                file.SaveAs(path);
                
                cafeDetails.cafeLogo = file.FileName;

                cafeDetailGateway.Update(cafeDetails);
                return RedirectToAction("ManageCafes");
            }
            return View(cafeDetails);
        }

        // GET: CafeDetails/EditOutlet/5
        // OUTLET
        [Authorize(Roles = "Admin")]
        public ActionResult EditOutlet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(cafeMapper.CafeOutletMap((int)id));
        }

        // POST: CafeDetails/EditOutlet/5
        // OUTLET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOutlet(OutletViewModel ovm)
        {
            if (ModelState.IsValid)
            {
                cafeMapper.UpdateOutlet(ovm);
                return RedirectToAction("ManageCafes");
            }
            return View(ovm);
        }

        // GET: CafeDetails/Delete/5
        // CAFE
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CafeViewModel cvm = cafeMapper.SelectOneCafe((int)id);
            int outletNum = cvm.CafeOutletVM.Count;
            if(outletNum < 2)
                @ViewBag.outletClass = "col-md-offset-4 col-md-4 center-block";
            else if (outletNum == 2)
                @ViewBag.outletClass = "col-md-offset-1 col-md-5 center-block";
            else
                @ViewBag.outletClass = "col-md-4 center-block";
            return View(cvm);
        }

        // POST: CafeDetails/Delete/5
        // CAFE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cafeMapper.DeleteCafe(id);
            return RedirectToAction("ManageCafes");
        }

        // GET: CafeDetails/Delete/5
        // OUTLET
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteOutlet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(cafeMapper.CafeOutletMap((int)id));
        }

        // POST: CafeDetails/DeleteOutlet/5
        // OUTLET
        [HttpPost, ActionName("DeleteOutlet")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOutletConfirmed(int id)
        {
            cafeMapper.DeleteOutlet(id);
            return RedirectToAction("ManageCafes");
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


            //Retrieving list of Carpark Information
            IEnumerable<CarPark> asd = carparkGateway.SelectAll();
            

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ViewBag.CarPark = oSerializer.Serialize(asd);
            
            return View("Index", mymodel);
        }

    }
}
