using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechReviewPoint.Models;
using System.IO;

namespace TechReviewPoint.Controllers
{
    public class ReviewsController : Controller
    {
        private tech_review_pointEntities db = new tech_review_pointEntities();

        // GET: Reviews
        public ActionResult Index()
        {

            int id = Convert.ToInt32(Session["UserSessionID"]);
            //Review re = db.Reviews.Find(id);
            var reviews = db.Reviews.Include(r => r.Product).Include(r => r.User);
            //.Where(m => m.UserID.Equals(id));


            //  var reviews = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.UserID.Equals(5));



            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);

        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,review_point,UserID,ProductID,ReviewPost,ReviewDate,Picture,review_img_file")] Review review)
        {
            //  Session["UserSessionID"] = user.UserID;
            // if (Session["UserSessionID"] != null) { }

            string fileName = Path.GetFileNameWithoutExtension(review.review_img_file.FileName);
            string extention = Path.GetExtension(review.review_img_file.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;

            review.Picture = "~/Review_Image/" + fileName;

            fileName = Path.Combine(Server.MapPath("~/Review_Image/"), fileName);

            review.review_img_file.SaveAs(fileName);



            if (ModelState.IsValid)
            {

                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", review.ProductID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", review.UserID);
            // ModelState.Clear();
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", review.ProductID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", review.UserID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,review_point,UserID,ProductID,ReviewPost,ReviewDate,Picture")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", review.ProductID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", review.UserID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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

        [HttpGet]
        public ActionResult review_Post_details(int id)
        {
            Review re = new Review();

            re = db.Reviews.Where(x => x.ReviewID == id).FirstOrDefault();
            return View(re);

        }
    }
}
