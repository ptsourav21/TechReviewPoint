using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechReviewPoint.Models
{
    public class Update_profile
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }


        public int UserPhone { get; set; }
        public string UserAdress { get; set; }
        public string UserImg { get; set; }
        
        public Nullable<decimal> tp_point { get; set; }
    }
}