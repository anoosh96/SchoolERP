using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MySchool.Models
{
    public class Token
    {
        
        [StringLength(255)]
        public string TokenVal { get; set; }

        public string UserID { get; set; }

        public String UserType { get; set; }

        [StringLength(255)]
        [Key]
        public String TokenKey { get; set; }
    }
}