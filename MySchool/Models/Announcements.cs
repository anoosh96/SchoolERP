using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySchool.Models
{
    public class Announcements
    {
        [Key]
        public DateTime Date { get; set; }

        public Class_ Class { get; set; }

        public Teacher Teacher { get; set; }

        public string Message { get; set; }
        
        public string Topic { get; set; }

        public string Recepient { get; set; }
        
        public string userID { get; set; }  

    }


}