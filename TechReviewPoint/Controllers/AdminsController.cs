using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using TechReviewPoint.Models;

namespace TechReviewPoint.Controllers
{
    public class AdminsController : Controller
    {
        // GET: Admins
        [HttpGet]
        public ActionResult AdminLogin()
        {
           // if (Session["AdminSessionEmail"] != null)
            
              //  Response.Redirect("~/Home/Index");
                return View();

        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Auth(Admin adminModel)
        {
            using(tech_review_pointEntities db = new tech_review_pointEntities())
            {
                var adminDetails = db.Admins.Where(x => x.AdminEmail == adminModel.AdminEmail && x.AdminPassword == adminModel.AdminPassword).FirstOrDefault();
               
                if(adminDetails==null)
                {
                    adminModel.LoginErrorMessage = "wrong email or password";
                    return View("AdminLogin", adminModel);

                }
                else
                {
                    Session["AdminSessionEmail"] = adminDetails.AdminEmail;

                    return RedirectToAction("AdminDashboard");
                }
            }
            return View();
        }
        public ActionResult AdminLogOut()
        {
            Session.Abandon();
            return RedirectToAction("AdminLogin");
        }
    }
}