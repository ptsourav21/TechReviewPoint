using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechReviewPoint.Controllers
{
    public class QandAController : Controller
    {
        // GET: QandA
        public ActionResult ques()
        {
            return View();
        }
    }
}