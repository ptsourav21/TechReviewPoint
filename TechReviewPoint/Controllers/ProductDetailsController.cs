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

            Product pro = db.Products.Find(id);
            Session["product_ID"] = pro.ProductID;

            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }

        public ActionResult search()
        {
            return View();
        }

        
    
        
    }

}