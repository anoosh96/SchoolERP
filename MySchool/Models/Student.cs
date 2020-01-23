using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySchool.Models
{
    public class Student
    {
        [Key]
        [RegularExpression("[c][0-9][0-9]+[AB]$", ErrorMessage = "Invalid ID Format")]
        public string StudentID { get; set; }

        [Required(ErrorMessage ="Please Enter The First Name")]
        [RegularExpression("^[a-zA-Z][a-zA-Z. ]+$",ErrorMessage ="Invalid Name")]
        public string FirstName { get; set; }

        
        [RegularExpression("^[a-zA-Z][a-zA-Z. ]+$", ErrorMessage = "Invalid Name")]
        public string LastName { get; set; }
        // public int StudentAge { get; set; }

       // public string FatherId { get; set; }

        
        public string Gender { get; set; }

        public Class_ Class { get; set; }

        public string Section { get; set; }

        [Required(ErrorMessage ="Date of Birth Required")]
        
        
        public DateTime? Dob { get; set; }

        public Parent Parent { get; set; }

        public string City { get; set; }

    }

       // public string StudentID { get; set; }


    
}