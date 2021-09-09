using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechReviewPoint.Models
{
    public class loginUser
    {
        [Required]
        [Display(Name =" Enter Email Address")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }


        [Required]
        [Display(Name ="Enter Password")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
}