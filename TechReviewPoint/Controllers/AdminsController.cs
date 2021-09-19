using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using TechReviewPoint.Models;
using System.Data.SqlClient;
using System.Web.Configuration;


namespace TechReviewPoint.Controllers
{
    public class AdminsController : Controller
    {



        private tech_review_pointEntities db = new tech_review_pointEntities();

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


        public ActionResult Count()
        {
            var TotalUserQuery = (from S in db.Users select S.UserID).Count();

            var TotalPassengersQuery = (from S in db.Reviews select S.UserID).Count();


            String TotalNextPassengersQuery = ("Select COUNT(*) From Reviews Inner Join Users On Reviews.UserID = Users.UserID");
            String TotalNextPassengersQuery1 = "";

            try
            {
                var connStr = WebConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;

               // string ConString = @"data source=SAQLAIN\SQLEXPRESS;initial catalog=EasyFlycomDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    SqlCommand cm = new SqlCommand(TotalNextPassengersQuery, connection);
                    connection.Open();
                    TotalNextPassengersQuery1 = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalNextPassengersQuery1 = e.ToString();
            }
            ViewBag.TotalUsers = TotalUserQuery;
            ViewBag.TotalPassengers = TotalPassengersQuery;
            ViewBag.TotalNextPassenger = TotalNextPassengersQuery1;


            return View();
        }


    }
}