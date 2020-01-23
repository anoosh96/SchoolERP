using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySchool.Models
{
    public class Notification
    {

        public string Status { get; set; }

        [Key]
        public string Time { get; set; }

        public string Recepient { get; set; }

        public string UserID { get; set; }

        public string SeenBy { get; set; }


    }
}