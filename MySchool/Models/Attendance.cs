using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySchool.Models
{
    public class Attendance
    {

        [Key Column(Order = 0)]

        public string AtdKey { get; set; }

        public Student Student { get; set; }

        public Class_ Class { get; set; }

        
        public string Date { get; set; }

        public Teacher Teacher { get; set; }

        public string AttendanceValue { get; set; }



    }


}