using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechReviewPoint.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult ReviewsInProductDetails()
        {
            return View();
        }
    }
}