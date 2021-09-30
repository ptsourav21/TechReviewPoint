using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechReviewPoint.Models;

namespace TechReviewPoint.Controllers
{
    public class PendingsController : Controller
    {
        private tech_review_pointEntities db = new tech_review_pointEntities();

        // GET: Pendings
        public ActionResult Index()
        {
            var pendings = db.Pendings.Include(p => p.User);
            return View(pendings.ToList());
        }

        // GET: Pendings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pending pending = db.Pendings.Find(id);
            if (pending == null)
            {
                return HttpNotFound();
            }
            return View(pending);
        }

        // GET: Pendings/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Pendings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PendingID,UserID,Mail,Date")] Pending pending)
        {
            pending.UserID = Convert.ToInt32(Session["UserSessionID"]);
            pending.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Pendings.Add(pending);
                db.SaveChanges();
                return RedirectToAction("ProductDashboard", "ProductDetails");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", pending.UserID);
            return View(pending);
        }

        // GET: Pendings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pending pending = db.Pendings.Find(id);
            if (pending == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", pending.UserID);
            return View(pending);
        }

        // POST: Pendings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PendingID,UserID,Mail,Date")] Pending pending)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pending).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", pending.UserID);
            return View(pending);
        }

        // GET: Pendings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pending pending = db.Pendings.Find(id);
            if (pending == null)
            {
                return HttpNotFound();
            }
            return View(pending);
        }

        // POST: Pendings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pending pending = db.Pendings.Find(id);
            db.Pendings.Remove(pending);
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
