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
            int id = Convert.ToInt32(Session["product_ID"]);
            var re = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.ProductID.Equals(id)).ToList();
            ViewData["re-view"] = re;
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
                return RedirectToAction("ReviewsInProductDetails","Review");
            }


            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", review.ProductID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", review.UserID);
            // ModelState.Clear();
            return View(review);
          //  return RedirectToAction("")

           // return View();
        }

        [HttpGet]
        public ActionResult ReviewDetails(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["Review_id"] = id;

            /*
            Review review = db.Reviews.Find(id);

            Session["Review_post"] = review.ReviewPost;
            Session["pic"] = review.Picture;
            Session["name"] = review.User.UserName;
            
           // Review d = db.Reviews.SqlQuery("Select * from Deals where BookingID = " + Convert.ToInt32(Session["bookingID"])).FirstOrDefault();

            if (review == null)
              {
                  return HttpNotFound();
              }
            */

            // var ads = db.Advertisements.SqlQuery("Select *from Advertisements where PriorityStatus = 1 or PriorityStatus = 2")
            //     .ToList<Advertisement>();
            // return View(review);

           // var re = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.ProductID.Equals(id)).ToList();

            var com = db.Comments.Include(c => c.Review).Include(c => c.User).Where(m => m.ReviewID==id).ToList();
            ViewData["co-ments"] = com;
            return View();
        }

        [HttpPost]
        public ActionResult ReviewDetails(Comment comment)
        {

            comment.UserID = Convert.ToInt32(Session["UserSessionID"]);
            comment.ReviewID = Convert.ToInt32(Session["Review_id"]);
            comment.CommentDate = DateTime.Today;

            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("ReviewDetails","Review");
            }

          //  ViewBag.ReviewID = new SelectList(db.Reviews, "ReviewID", "ReviewPost", comment.ReviewID);
           // ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", comment.UserID);
            return View(comment);
        }

        [HttpGet]
        public ActionResult productIssues()
        {
            int id = Convert.ToInt32(Session["product_ID"]);
          //  var re = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.ProductID.Equals(id)).ToList();
          
            
            var issues = db.Issues.Include(i => i.Product).Include(i => i.User).Where(i => i.ProductID.Equals(id)).ToList();
            // return View(issues.ToList());
            ViewData["is-sue"] = issues;
            return View();
        }


        [HttpPost]
        public ActionResult productIssues(Issue issue)
        {
            issue.UserID = Convert.ToInt32(Session["UserSessionID"]);
            //review.review_img_file.SaveAs(fileName);
            issue.IssueDate = DateTime.Today;
            issue.ProductID = Convert.ToInt32(Session["product_ID"]);

            if (ModelState.IsValid)
            {
                db.Issues.Add(issue);
                db.SaveChanges();
                return RedirectToAction("productIssues", "Review");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", issue.ProductID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", issue.UserID);
            return View(issue);
        }

    }
}