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
    public class ProductDetailsController : Controller
    {
        private tech_review_pointEntities db = new tech_review_pointEntities();

        loginUser abc = new loginUser();

        // GET: Products

        public ActionResult ProductDashboard()
        {

            var products = db.Products.Include(q => q.Category);
            return View(products.ToList());

        }

        // GET: ProductDetails
        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Session["product_ID"] = id;


            Product pro = db.Products.Find(id);
            Session["product_ID"] = pro.ProductID;

            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }

        
        /* 
        [HttpGet]
        public ActionResult AddingReview(int? idp)
        {
            if (idp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var re = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.ProductID.Equals(idp)).ToList();

            // var reviews = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.UserID.Equals(id));

            ViewData["re-view"] = re;
            // return View(reviews.ToList());
            return View();

        }

        // GET: Review
        [HttpPost]
        public ActionResult AddingReview(Review review)
        {


            string fileName = Path.GetFileNameWithoutExtension(review.review_img_file.FileName);
            string extention = Path.GetExtension(review.review_img_file.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;

            review.Picture = "~/Review_Image/" + fileName;

            fileName = Path.Combine(Server.MapPath("~/Review_Image/"), fileName);
            review.UserID = Convert.ToInt32(Session["UserSessionID"]);
            review.review_img_file.SaveAs(fileName);
            review.ReviewDate = DateTime.Now;
            review.ProductID = Convert.ToInt32(Session["product_ID"]);


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
        */
        
    }

}