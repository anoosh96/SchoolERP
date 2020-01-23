using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySchool.Models;

namespace MySchool.Controllers
{
    public class FinanceController : Controller
    {

        private SchoolContext school = new SchoolContext();
        // GET: Finance
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddDue()
        {
            Due d = new Due();
            d.Student = new Student();
            d.Student.StudentID = "";
            return View(d);
        }

        [HttpPost]
        public ActionResult AddDue(FormCollection frm)
        {
            Due d = new Due();
            string sid = frm["Student.StudentID"].ToString();

            d.Student = school.Students.Find(sid);
            if(d.Student == null)
            {
                ModelState.AddModelError("Student.StudentID", "No Such Student Exists");
                
            }
            d.Status = frm["Status"].ToString();
            d.Amount = Double.Parse(frm["Amount"].ToString());
            d.DueType = frm["DueType"].ToString();

            DateTime dt = DateTime.Parse(frm["DueDate"]);
            if(dt.CompareTo(DateTime.Now) < 0)
            {
                ModelState.AddModelError("DueDate", "Invalid Date");
            }
            d.DueDate = dt.ToShortDateString();
            if(d.Student != null)
            {
                d.DueId = d.Student.StudentID + d.DueDate;
                if (school.Dues.Find(d.DueId) != null)
                {
                    ModelState.AddModelError("DueId", "Due Already Exists");
                }
            }
            
            TryUpdateModel(d);

            if (ModelState.IsValid)
            {
                school.Dues.Add(d);
                school.SaveChanges();
                TempData["status"] = "ok";

                return RedirectToAction("ShowDue");
            }

            return View(d);
        }


        public ActionResult ShowDue()
        {

            return View();

        }


        [HttpGet]
        public ActionResult EditDue(string id)
        {
            if(Session["username"] == null)
            {
                return RedirectToAction("login", "User");
            }
            Due d = school.Dues.Include("Student").FirstOrDefault(m => m.DueId.Equals(id));
            return View(d);
        }

        [HttpPost]
        public ActionResult EditDue(FormCollection frm)
        {
            string dueid = frm["DueId"].ToString();

            Due d = school.Dues.Find(dueid);
            string sid = frm["Student.StudentID"].ToString();
            d.Student = school.Students.Find(sid);
            if (d.Student == null)
            {
                ModelState.AddModelError("Student.StudentID", "No Such Student Exists");

            }
            d.Status = frm["Status"].ToString();
            d.Amount = Double.Parse(frm["Amount"].ToString());
            DateTime dt = DateTime.Parse(frm["DueDate"]);
            /*if (dt.CompareTo(DateTime.Now) < 0)
            {
                ModelState.AddModelError("DueDate", "Invalid Date");
            }*/
            d.DueDate = dt.ToShortDateString();
            if (d.Student != null)
            {
                d.DueId = d.Student.StudentID + d.DueDate;
                
            }

            TryUpdateModel(d);

            if (ModelState.IsValid)
            {
                
                school.SaveChanges();
                TempData["status"] = "ok";

                return RedirectToAction("AddDue");
            }

            return View(d);
        }



        [HttpGet]
        public ActionResult AddExpense()
        {

            return View();
        }


        [HttpPost]
        public ActionResult AddExpense(Expense e)
        {

            if (ModelState.IsValid)
            {
                school.Expenses.Add(e);
                school.SaveChanges();

                return RedirectToAction("AdminHome", "User");
            }

            return View(e);
        }

        public ActionResult ViewExpense()
        {
            if(Session["Username"].ToString() == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(school.Expenses.ToList());
        }

    }
}