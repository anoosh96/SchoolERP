using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySchool.Models
{
    public class Due
    {
        [Key]
        public string DueId { get; set; }   //StudentId+DueDate

        public Student Student { get; set; }

        public double Amount { get; set; }

        public string DueDate { get; set; }

        public string Status { get; set; }   //paid or due

        public string DueType { get; set; }
    }
}