using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MySchool.Models
{
    public class Expense
    {
       

        [Key]
        public string ExpenseID { get; set; }

        public string Type { get; set; }

        public double Amount { get; set; }

        
        public DateTime Date { get; set; }

        public string Status { get; set; }
    }
}