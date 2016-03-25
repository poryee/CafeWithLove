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
    public class CafeSuggestionsController : Controller
    {
        private CafeWithLoveContext db = new CafeWithLoveContext();

        // GET: CafeSuggestions
        public ActionResult Index()
        {
            return View(db.CafeSuggestions.ToList());
        }

        // GET: CafeSuggestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CafeSuggestion cafeSuggestion = db.CafeSuggestions.Find(id);
            if (cafeSuggestion == null)
            {
                return HttpNotFound();
            }
            return View(cafeSuggestion);
        }

        // GET: CafeSuggestions/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CafeSuggestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,userId,cafeName,cafeWebsite,cafeDesc")] CafeSuggestion cafeSuggestion)
        {

            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                cafeSuggestion.userId = userId;
                db.CafeSuggestions.Add(cafeSuggestion);
                db.SaveChanges();
                return RedirectToAction("Create", new { cafeName = cafeSuggestion.cafeName });
            }

            return View(cafeSuggestion);
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
