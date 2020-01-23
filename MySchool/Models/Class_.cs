using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySchool.Models
{
    public class Class_
    {
        [Key]
        public string ClassID { get; set; }

        public string Section { get; set; }

        public int Strength { get; set; }

        public Teacher Teacher { get; set; }



    }


}