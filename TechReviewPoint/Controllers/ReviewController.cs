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
    public class ReviewController : Controller
    {
        private tech_review_pointEntities db = new tech_review_pointEntities();

        


        [HttpGet]
        public ActionResult ReviewsInProductDetails()
        {
            //var reviews = db.Reviews.Include(r => r.Product).Include(r => r.User);
            int id = Convert.ToInt32(Session["product_ID"]);

            var re = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.UserID.Equals(id)).ToList();

           // var reviews = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.UserID.Equals(id));

            ViewData["re-view"] = re;
            // return View(reviews.ToList());
            return View();
        }




        // GET: Review
        [HttpPost]
        public ActionResult ReviewsInProductDetails(Review review)
        {
            // Review review = new Review();
            //int id = Convert.ToInt32(Session["UserSessionID"]);
           


            string fileName = Path.GetFileNameWithoutExtension(review.review_img_file.FileName);
            string extention = Path.GetExtension(review.review_img_file.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;

            review.Picture = "~/Review_Image/" + fileName;

            fileName = Path.Combine(Server.MapPath("~/Review_Image/"), fileName);
            review.UserID = Convert.ToInt32(Session["UserSessionID"]);
            review.review_img_file.SaveAs(fileName);
          review.ReviewDate= DateTime.Now;
            review.ProductID = Convert.ToInt32(Session["product_ID"]);
//db.Reviews.InsertOnSubmit(review);


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


           // return View();
        }
    }
}