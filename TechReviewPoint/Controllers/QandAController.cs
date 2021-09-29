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
    public class QandAController : Controller
    {

        private tech_review_pointEntities db = new tech_review_pointEntities();

        loginUser abc = new loginUser();
        // GET: QandA
        public ActionResult ques()
        {
            // int id = Convert.ToInt32(Session["UserSessionID"]);

            var que = db.Questions.SqlQuery("Select *from Questions")
                  .ToList<Question>();


            ViewData["QUE"] = que;
            return View();
        }

        [HttpPost]
        public ActionResult ques(Question question)
        {

            question.UserID = Convert.ToInt32(Session["UserSessionID"]);
            question.QuestionDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("ques", "QandA");
            }

            return View(question);
        }
    }
}