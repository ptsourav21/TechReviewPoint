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
    public class UsersController : Controller
    {
        private tech_review_pointEntities db = new tech_review_pointEntities();

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
                return Content("Registration Complete");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult UserDashboard()
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
                    // return RedirectToAction("UserDashboard", new { email = user.UserEmail });
                    return RedirectToAction("UserDashboard");

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
            return RedirectToAction("Login");
        }






        // GET: Users/Details/5





    }
}
