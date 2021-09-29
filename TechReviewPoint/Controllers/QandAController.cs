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

        [HttpGet]
        public ActionResult ans(int? id)
        {
            //  var re = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.ProductID.Equals(id)).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["Question_Id"] = id;

            //var answ = db.Answers.Include(a => a.Question).Include(a => a.User).Where(a => a.QuestionID.Equals(id)).ToList();

            var answ = db.Answers.SqlQuery("Select  *from Answers").ToList<Answer>();

/*
            var applyJobs = (from a in db.Answers
                             join s in db.Questions on a.QuestionID equals s.QuestionID
                             where s.QuestionID == id
                             select a).ToList();
*/
           // return View(applyJobs);

            ViewData["ANS"] = answ;

            //var com = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.ReviewID.Equals(id)).ToList();

           // var qu = db.Questions.Include(q => q.User).Where(q => q.QuestionID.Equals(id)).FirstOrDefault();
//var qu = db.Answers.Include(a => a.Question).Include(a => a.User).Where(a => a.QuestionID.Equals(id)).ToList();

            return View();

        }

        [HttpPost]
        public ActionResult ans(Answer answer)
        {
            answer.UserID = Convert.ToInt32(Session["UserSessionID"]);
            answer.QuestionID= Convert.ToInt32(Session["Question_Id"]);
            answer.AnswerDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("ans", "QandA");
            }

            return View(answer);
        }
    }
}