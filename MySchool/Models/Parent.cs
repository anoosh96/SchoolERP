using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MySchool.Models
{
    public class Parent
    {
        [Key]
        public string ParentId {get; set;}

        
        [RegularExpression("^[a-zA-Z][a-zA-Z. ]+$", ErrorMessage = "Invalid Name")]
        public string ParentName { get; set;}

       // [RegularExpression("[a-zA-Z.,-\\s]+$", ErrorMessage = "Invalid Address")]
        //[Required(ErrorMessage = "Please Enter The Address")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "Please Enter The Contact Number")]
        //[RegularExpression("^[0-9+][0-9]+$", ErrorMessage = "Invalid Number")]
        //[StringLength(14)]
        public string Contact { get; set; }


    }
}