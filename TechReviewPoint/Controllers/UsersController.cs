using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
            return View();
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


        [HttpGet]
        public ActionResult UpdateProfile(int? id)
        {
            /*                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                } 
                */
                return View();
            }


       /* public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
       */


        [HttpPost]
        public ActionResult UpdateProfile(Update_profile user)
        {

                string email = Convert.ToString(Session["UserSessionEmail"]);

                var info = db.Users.Where(u => u.UserEmail.Equals(email)).FirstOrDefault();

            

            return View(info);
        }




        public ActionResult UserDashboard()
        {
            string email = Convert.ToString(Session["UserSessionEmail"]);
            var info = db.Users.Where(u => u.UserEmail.Equals(email)).FirstOrDefault();
            return View(info);


        }

       

    }
}
