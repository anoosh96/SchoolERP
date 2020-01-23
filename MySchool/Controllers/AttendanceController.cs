using MySchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySchool.Controllers
{
    public class AttendanceController : Controller
    {
        private SchoolContext school = new SchoolContext();
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MarkAttendance()
        {
            if(Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            string tid = Session["username"].ToString();
            return View(from n in school.Students.Include("Class") where n.Class.Teacher.TeacherID.Equals(tid) select n);
        }

        [HttpPost]
        public ActionResult MarkAttendance(FormCollection frm)
        {
            Attendance atd;
            int x = Convert.ToInt32(frm["noofstudents"].ToString());
            string cid ="";
            string sid = "";
            string tid="";
            try
            {


                for (int i = 0; i < x; i++)
                {
                    atd = new Attendance();
                    cid = frm["classid["+i+"]"].ToString();
                    sid = frm["studentid[" + i + "]"].ToString();
                    atd.Class = school.Class.FirstOrDefault(m => m.ClassID.Equals(cid));
                    atd.Student = school.Students.FirstOrDefault(m => m.StudentID.Equals(sid));
                    atd.Date = DateTime.Now.Date.ToShortDateString();
                    atd.AtdKey = sid + atd.Date;
                    atd.AttendanceValue = frm["attendance[" + i + "]"].ToString();
                    tid = frm["tid"].ToString();
                    atd.Teacher = school.Teachers.FirstOrDefault(m => m.TeacherID.Equals(tid));

                    TryUpdateModel(atd);
                    if (school.Attendance.Find(atd.AtdKey) != null)
                    {
                        ModelState.AddModelError("Atdkey", "Attendance Already Submitted for the Day");
                    }
                    if (ModelState.IsValid)
                    {
                        school.Attendance.Add(atd);
                        school.SaveChanges();
                        
                        
                    }
                    else
                    {
                        ViewBag.Message = "Attendance Already Exists";
                        return View(from n in school.Students.Include("Class") where n.Class.Teacher.TeacherID.Equals(tid) select n);
                    }
                }
                TempData["res"] = "added";
                return RedirectToAction("TeacherHome", "User");
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("ErrorView");
            }
            
        }

        public ActionResult ViewAttendanceParent()
        {
            string pid = Session["username"].ToString();
            return View(from n in school.Attendance.Include("Student").Include("Class") where n.Student.Parent.ParentId.Equals(pid) select n);
        }

        public ActionResult ViewAttendance()
        {
            return View();
        }

        public ActionResult ViewAttendanceTeacher()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string tid = Session["username"].ToString();
            return View(from n in school.Attendance.Include("Student").Include("Class") where n.Teacher.TeacherID.Equals(tid) select n);
        }


        public ActionResult ViewAttendanceStudent()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("User", "StudentHome");

            }

            string sid = Session["username"].ToString();
            return View(from n in school.Attendance.Include("Student").Include("Class") where n.Student.StudentID.Equals(sid) select n);
        }

    }
}