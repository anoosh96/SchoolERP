using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySchool.Models
{
    public class Teacher
    {
        [Key]
       [Required(ErrorMessage = "Please Enter The ID")]
        public string TeacherID { get; set; }

       [Required(ErrorMessage = "Please Enter The Name")]
       [RegularExpression("^[a-zA-Z][a-zA-Z. ]+$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        public string City { get; set;}

        public string Gender { get; set; }


    }


}

