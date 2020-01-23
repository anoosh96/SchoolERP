using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySchool.Models;

namespace MySchool.Controllers
{
    public class ScheduleController : Controller
    {
        private SchoolContext school = new SchoolContext();

        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AddSchedule()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddSchedule(FormCollection frm)
        {
            Schedule sc = new Schedule();
            string clasid = frm["ClassID"].ToString()+frm["Section"].ToString();
            string teacherid = frm["TeacherId"].ToString();
            
            try
            {


                sc.Class = school.Class.FirstOrDefault(m => m.ClassID.Equals(clasid));
                sc.Teacher = school.Teachers.FirstOrDefault(m => m.TeacherID.Equals(teacherid));
                
                sc.Day = frm["Day"].ToString();
                sc.Time = Convert.ToString(frm["Time"]);
                sc.Subject = Convert.ToString(frm["Subject"]);
                sc.ScheduleId = sc.Class.ClassID + sc.Subject;
                Schedule sc2 = school.Schedules.Find(sc.ScheduleId);

                if (sc2 != null)
                {
                    ModelState.AddModelError("Subject", "Schedule Already exists");
                }
                TryUpdateModel(sc);
                if (sc.Teacher == null)
                {
                    ModelState.AddModelError("Teacher.TeacherID", "No Such Teacher Exists!");
                }
                if (ModelState.IsValid)
                {
                    school.Schedules.Add(sc);
                    school.SaveChanges();
                    return RedirectToAction("index","User");
                }
                else
                {
                    return View(sc);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("ErrorView");
            }
            
        }


        public ActionResult ViewSchedule()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }

        public ActionResult ViewScheduleStudent()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string stdid = Session["username"].ToString();
            Student s = school.Students.Include("Class").FirstOrDefault(m => m.StudentID.Equals(stdid));
            string classid = s.Class.ClassID;
            
            return View((from n in school.Schedules.Include("Class").Include("Teacher") where n.Class.ClassID == classid select n));
        }

        public ActionResult ViewScheduleTeacher()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string tid = Session["username"].ToString();
            return View(from n in school.Schedules.Include("Class").Include("Teacher") where n.Teacher.TeacherID.Equals(tid) select n);
        }



    }
}