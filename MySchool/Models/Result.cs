using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySchool.Models
{
    public class Result
    {

        [Key]

        public string ResultId { get; set; }

        public Student Student { get; set; }

        public string Date { get; set; }

        public string Subject { get; set; }

        public Class_ Class { get; set; }

        public string Type { get; set; }

        [Range(10, 100,
       ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public float TotalMarks { get; set; }

       
        public float MarksObtained { get; set; }



    }
}