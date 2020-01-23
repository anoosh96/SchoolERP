using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySchool.Models
{
    public class FcmMessage
    {


        public string[] registration_ids { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
        public object data { get; set; }

    }
}