using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MySchool.Models
{
    public class User
    {
        [Key]
        public string UserID { get; set; }

        public string UserType { get; set; }


        public string UserPassword { get; set; }
    }
}