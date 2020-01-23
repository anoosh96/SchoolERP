using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySchool.Models;

namespace MySchool.Controllers
{
    public class CommunicationController : Controller
    {
        private SchoolContext school = new SchoolContext();
        // GET: Communication
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAnnouncement()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddAnnouncement(FormCollection frm)
        {
            Announcements anc = new Announcements();
            string tid = Session["username"].ToString();
            string cid = frm["ClassID"].ToString();
            anc.Teacher = school.Teachers.FirstOrDefault(m => m.TeacherID.Equals(tid));
            anc.Date = DateTime.Now;
            anc.Message = frm["Message"].ToString();
            anc.Topic = frm["Topic"].ToString();
            anc.Class = school.Class.FirstOrDefault(m => m.ClassID.Equals(cid));
            anc.Recepient = "student";
            anc.userID = frm["UserID"].ToString();

            TryUpdateModel(anc);

            if (frm["UserID"].ToString().Length != 0)
            {
                anc.Recepient = frm["UserID"].ToString();
                if (school.Students.Include("Class").FirstOrDefault(m=>m.StudentID.Equals(anc.userID) && m.Class.ClassID.Equals(cid)) == null)
                {
                    ModelState.AddModelError("userID", "no such Student in this class");
                }

            }
            else
            {
                anc.Recepient = frm["Recepient"].ToString();
            }

            if (ModelState.IsValid)
            {
                Notification n = new Notification();
                n.Recepient = anc.Recepient;
                n.SeenBy = "";
                n.Status = "unseen";
                n.UserID = anc.userID;
                n.Time = DateTime.Now.ToString();

                

                school.Announcements.Add(anc);
                school.Notifications.Add(n);
                TempData["res"] = "added";
                school.SaveChanges();
                return RedirectToAction("TeacherHome","User");
            }
            return View(anc);
        }


        public ActionResult ViewAnnouncements()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View(school.Announcements.ToList());

        }


        public ActionResult ViewClassAnnouncements()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string tid = Session["username"].ToString();

            return View(from n in school.Announcements.Include("Class").Include("Teacher") where n.Teacher.TeacherID.Equals(tid) select n);
        }

        public ActionResult ViewClassAnnouncementsStudent()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string sid = Session["username"].ToString();
            Student s = school.Students.Include("Class").FirstOrDefault(m => m.StudentID.Equals(sid));

            return View((from n in school.Announcements.Include("Class").Include("Teacher") where n.Class.ClassID.Equals(s.Class.ClassID) select n).OrderByDescending(m=>m.Date));
        }




        public ActionResult ViewGeneralAnnouncements()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string uid = Session["username"].ToString();

            return View((from n in school.Announcements where n.Recepient.Equals("teacher") || n.Recepient.Equals("everyone") || n.userID.Equals(uid) select n).OrderByDescending(m=>m.Date));
        }

        public ActionResult ViewGeneralAnnouncementsStudent()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string uid = Session["username"].ToString();

            return View((from n in school.Announcements where n.Recepient.Equals("student") || n.Recepient.Equals("everyone") || n.userID.Equals(uid) select n).OrderByDescending(m=>m.Date));
        }


        public ActionResult ViewAnnouncementsParent()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            string uid = Session["username"].ToString();
            return View((from n in school.Announcements where n.Recepient.Equals("parent") || n.userID.Equals(uid) || n.Recepient.Equals("everyone") select n).OrderByDescending(m=>m.Date));
        }


        [HttpGet]
        public ActionResult AddAnnouncementsAdmin()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            Announcements a = new Announcements();
            return View(a);
        }

        [HttpPost]
        public ActionResult AddAnnouncementsAdmin(FormCollection frm)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Announcements anc = new Announcements();
            //string tid = Session["username"].ToString();
            //string cid = frm["ClassID"].ToString();

            anc.Date = DateTime.Now;
            anc.Message = frm["Message"].ToString();
            anc.Topic = frm["Topic"].ToString();
            //anc.Class = school.Class.FirstOrDefault(m => m.ClassID.Equals(cid));
            //anc.Recepient = frm["Recepient"].ToString();
            anc.userID = "";
            
            anc.userID = frm["UserID"].ToString();
            

            TryUpdateModel(anc);
            if (frm["UserID"].ToString().Length != 0)
            {
                anc.Recepient = frm["UserID"].ToString();
                if (school.Users.Find(anc.userID) == null)
                {
                    ModelState.AddModelError("userID", "no such user");
                }

            }
            else
            {
                anc.Recepient = frm["Recepient"].ToString();
            }

            if (ModelState.IsValid)
            {
                Notification n = new Notification();
                n.Recepient = anc.Recepient;
                n.Status = "unseen";
                n.Time = DateTime.Now.ToString();
                n.UserID = anc.userID;
                n.SeenBy = "";
                String[] tkns;
                //TryUpdateModel(anc);
                if (n.Recepient.Equals("everyone"))
                {
                    tkns = (from m in school.Tokens select m.TokenVal).ToArray();
                }
                else {
                     tkns = (from m in school.Tokens where m.UserID == anc.Recepient || m.UserType == anc.Recepient select m.TokenVal).ToArray();
                }

                if(tkns.Length > 0)
                {
                    PushNotification.send(anc.Message, tkns);
                }
                

                school.Announcements.Add(anc);
                school.Notifications.Add(n);

                school.SaveChanges();
                TempData["res"] = "added";
                return RedirectToAction("AdminHome","User");
            }
            
            return View(anc);
        }

        
    }
}