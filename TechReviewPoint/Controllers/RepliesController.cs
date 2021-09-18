using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechReviewPoint.Models;

namespace TechReviewPoint.Controllers
{
    public class RepliesController : Controller
    {
        private tech_review_pointEntities db = new tech_review_pointEntities();

        // GET: Replies
        public ActionResult Index()
        {
            var replies = db.Replies.Include(r => r.Issue).Include(r => r.User);
            return View(replies.ToList());
        }

        // GET: Replies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            return View(reply);
        }

        // GET: Replies/Create
        public ActionResult Create()
        {
            ViewBag.IssueID = new SelectList(db.Issues, "IssueID", "IssuePost");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Replies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReplyID,UserID,IssueID,Reply1,ReplyDate")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                db.Replies.Add(reply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IssueID = new SelectList(db.Issues, "IssueID", "IssuePost", reply.IssueID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", reply.UserID);
            return View(reply);
        }

        // GET: Replies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            ViewBag.IssueID = new SelectList(db.Issues, "IssueID", "IssuePost", reply.IssueID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", reply.UserID);
            return View(reply);
        }

        // POST: Replies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReplyID,UserID,IssueID,Reply1,ReplyDate")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IssueID = new SelectList(db.Issues, "IssueID", "IssuePost", reply.IssueID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", reply.UserID);
            return View(reply);
        }

        // GET: Replies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reply reply = db.Replies.Find(id);
            db.Replies.Remove(reply);
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
    }
}
