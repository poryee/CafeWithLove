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
        private CafeWithLoveContext db = new CafeWithLoveContext();

        // GET: CafeDetails
        public ActionResult Index()
        {
            return View(db.CafeDetails.ToList());
        }

        // GET: CafeDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CafeDetail cafeDetails = db.CafeDetails.Find(id);
            if (cafeDetails == null)
            {
                return HttpNotFound();
            }
            return View(cafeDetails);
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
                db.CafeDetails.Add(cafeDetails);
                db.SaveChanges();
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
            CafeDetail cafeDetails = db.CafeDetails.Find(id);
            if (cafeDetails == null)
            {
                return HttpNotFound();
            }
            return View(cafeDetails);
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
                db.Entry(cafeDetails).State = EntityState.Modified;
                db.SaveChanges();
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
            CafeDetail cafeDetails = db.CafeDetails.Find(id);
            if (cafeDetails == null)
            {
                return HttpNotFound();
            }
            return View(cafeDetails);
        }

        // POST: CafeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CafeDetail cafeDetails = db.CafeDetails.Find(id);
            db.CafeDetails.Remove(cafeDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
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
