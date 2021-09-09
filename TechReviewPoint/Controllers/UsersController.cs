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

        [HttpPost]
        public ActionResult Login(loginUser login)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(u => u.UserEmail.Equals(login.UserEmail)
                 && u.UserPassword.Equals(login.UserPassword)).FirstOrDefault();

                if (user != null)
                {
                    return Content("Login Successfull!!");
                }
                else
                {
                    return Content(" Login Failed!!");
                }
            }
            return View();
        }


        // GET: Users/Details/5

    }
}
