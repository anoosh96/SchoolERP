using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MySchool.Models
{
    public class Schedule
    {
        [Key]
        public string ScheduleId { get; set; } 

        public Class_ Class { get; set; }

        
        public string Subject { get; set; }

        public Teacher Teacher { get; set; }

        [Required]
        //[DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string Time { get; set; }
        

        public string Day { get; set; }


    }
}