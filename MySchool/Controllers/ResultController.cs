using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySchool.Models;
using System.Data.Entity;

namespace MySchool.Controllers
{
    public class ResultController : Controller
    {

        private SchoolContext school = new SchoolContext();
        // GET: Result
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddResult()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddResult(FormCollection frm)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            int x = Convert.ToInt32(frm["noOfStudents"].ToString());
            Result res;


            for (int i = 0; i < x; i++)
            {
                res = new Result();
                string stdid = frm["StudentId" + i].ToString();
                string cid = frm["ClassID"].ToString();
                res.Student = school.Students.FirstOrDefault(m => m.StudentID.Equals(stdid));
                res.Date = DateTime.Now.ToString();
                res.MarksObtained = float.Parse(frm["MarksObtained" + i].ToString());
                res.TotalMarks = float.Parse(frm["TotalMarks" + i].ToString());
                res.Subject = frm["Subject"].ToString();

                res.Class = school.Class.FirstOrDefault(m => m.ClassID.Equals(cid));
                res.Type = frm["Type"].ToString();
                res.ResultId = stdid + res.Type + res.Subject;
                if (school.Results.Find(res.ResultId) != null)
                {
                    ModelState.AddModelError("ResultId", "Result already Exists");
                }
                TryUpdateModel(res);
                if (ModelState.IsValid )
                {
                    school.Results.Add(res);
                    school.SaveChanges();
                    
                }
                else
                {
                    return View(res);
                }
                
            }
            TempData["res"] = "added";
            return RedirectToAction("TeacherHome", "User");
        }


        [HttpGet]
        public ActionResult EditResult(string id)
        {
            Result r = new Result();

            r = school.Results.Find(id);

            return View(r);

        }


        [HttpPost]
        public ActionResult EditResult(Result r)
        {

            if (ModelState.IsValid)
            {
                school.Entry(r).State = EntityState.Modified;

                school.SaveChanges();
                TempData["status"] = "edited";
                return RedirectToAction("ViewResultTeacher");
            }

           // return View(t);
            return View(r);

        }







        public ActionResult ViewResult()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(from n in school.Results select n);
        }


        public ActionResult ViewResultTeacher()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            
            return View();
        }

        public ActionResult ViewResultParent()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string pid = Session["username"].ToString();
            return View();
        }

        public ActionResult ViewResultStudent()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string sid = Session["username"].ToString();
            return View(school.Results.Include("Student").Where(m=>m.Student.StudentID.Equals(sid)));
        }



    }
}