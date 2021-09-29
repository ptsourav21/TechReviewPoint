using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechReviewPoint.Models
{
    public class TempSearch
    {
        [DataType(DataType.Text)]
      [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        // [Required]
       // [DataType(DataType.Text)]
       // [Display(Name = "Selling Product")]
       [DataType(DataType.Text)]
       [Display(Name ="Category Name")]
        public string CategoryName { get; set; }

        [Display(Name ="Product Price")]
        public decimal ProductPrice { get; set; }

    }
}