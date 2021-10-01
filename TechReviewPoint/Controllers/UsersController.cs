using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechReviewPoint.Models;

namespace TechReviewPoint.Controllers
{
    public class UsersController : Controller
    {
        private tech_review_pointEntities db = new tech_review_pointEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
       
        public ActionResult Profile()
        {
            int id = Convert.ToInt32(Session["UserSessionID"]);
            var reviews = db.Reviews.Include(r => r.Product).Include(r => r.User).Where(m => m.UserID.Equals(id));
            
            User us = db.Users.Where(m => m.UserID.Equals(id)).FirstOrDefault();
            ViewData["user_info"] = us;

            return View(reviews.ToList());

        //    System.Diagnostics.Debug.WriteLine(us.UserImg);


        }
        public ActionResult point(User user)
        {
            //var total_review = ("SELECT review_point FROM Reviews where UserID = " + cu.UserID);

            int id = Convert.ToInt32(Session["UserSessionID"]);

            var total_review = ("SELECT COUNT(*) FROM Reviews where UserID = " + id);

            return View();
        }


        [HttpGet]
        public ActionResult profileUpdate()
        {
            //   if (Session["UserSessionID"] == null)
            //   return RedirectToAction("Index", "Home");
            int id = Convert.ToInt32(Session["UserSessionID"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }

        [HttpPost]
        public ActionResult profileUpdate([Bind(Include = "UserID,UserName,UserEmail,UserPassword,UserPhone,UserAdress,tp_point,UserImg,user_img_file")] User user)
        {
            
            string fileName = Path.GetFileNameWithoutExtension(user.user_img_file.FileName);
            string extention = Path.GetExtension(user.user_img_file.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;

            user.UserImg = "~/profile_pic/" + fileName;

            fileName = Path.Combine(Server.MapPath("~/profile_pic/"), fileName);

            user.user_img_file.SaveAs(fileName);
            try
            {
                var cu = db.Users.Find(Convert.ToInt32(Session["UserSessionID"]));
                // user.UserEmail = "" + cu.UserEmail;
                // user.Rating = cu.Rating;
                db.Database.ExecuteSqlCommand("Update Users set UserName = '" + user.UserName + "' ,  UserImg = '" + user.UserImg + "', UserPhone = '" + user.UserPhone + "', UserAdress = '" + user.UserAdress + "' where UserID = " + cu.UserID);

                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }


            return RedirectToAction("Profile", "Users");
        }




        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View("test");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,UserEmail,UserPassword,UserPhone,UserAdress,tp_point")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("test", user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,UserEmail,UserPassword,UserPhone,UserAdress,tp_point")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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


        // GET: Users
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User u)
        {
            if(ModelState.IsValid)
            {
                db.Users.Add(u);
                db.SaveChanges();
                //Next page
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

       

        [HttpPost]
        public ActionResult Login(loginUser login)
        {
            
                if (ModelState.IsValid)
                {
                    var user = db.Users.Where(u => u.UserEmail.Equals(login.UserEmail)
                     && u.UserPassword.Equals(login.UserPassword)).FirstOrDefault();

                if (user != null)
                {
                    Session["UserSessionEmail"] = user.UserEmail;
                    Session["UserSessionID"] = user.UserID;
                    Session["UserSessionName"] = user.UserName;

                    //return RedirectToAction("UserDashboard", new { email = user.UserEmail });
                    //  return RedirectToAction("UserDashboard");
                    return RedirectToAction("ProductDashboard", "ProductDetails", new { area = "" });

                }
                else
                {

                    ViewBag.Loginfailed = "User not found or Password mismatch";
                    return View();

                }

                }
            
            return View();
        }
        public ActionResult UserLogout()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }

        public ActionResult UserDashboard()
        {
            string email = Convert.ToString(Session["UserSessionEmail"]);
            var info = db.Users.Where(u => u.UserEmail.Equals(email)).FirstOrDefault();
            return View(info);

        }
  

    }
}
