using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechReviewPoint.Models;

namespace TechReviewPoint.Controllers
{
    public class ReviewDetailsController : Controller
    {
        private tech_review_pointEntities db = new tech_review_pointEntities();

        // GET: ReviewDetails
        public ActionResult ReviewDetails()
        {


           /* 
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
            */
            return View();
        }
    }
}