using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySchool.Models;


namespace MySchool.Controllers
{

    
    public class DemoController : ApiController
    {
        private SchoolContext school = new SchoolContext();
        [ActionName("GetStudents")]
        public IHttpActionResult GetStudents()
        {
            
            return Ok(school.Students.Include("Parent").Include("Class").ToList());
        }

        [ActionName("GetTeachers")]
        public IHttpActionResult GetTeachers()
        {

            return Ok(school.Teachers.ToList());
        }

        [ActionName("GetAttend")]
        public IHttpActionResult GetAttend()
        {

            return Ok(school.Attendance.Include("Student").Include("Class").ToList());
        }


        [ActionName("GetAttendT")]
        public IHttpActionResult GetAttendT(string id)
        {

            return Ok(school.Attendance.Include("Student").Include("Class").Where(m=>m.Teacher.TeacherID.Equals(id)));
        }


        [ActionName("GetDues")]
        public IHttpActionResult GetDues()
        {

            return Ok(school.Dues.Include("Student").ToList());
        }


        [ActionName("GetStdSchedule")]
        public IHttpActionResult GetStdSchedule(string id)
        {
            
            return Ok(school.Schedules.Include("Class").Include("Teacher").Where(m=>m.Class.ClassID.Equals(id)).ToList());
        }

        [ActionName("GetResTeacher")]
        public IHttpActionResult GetResTeacher(string id)
        {
            
            return Ok(school.Results.Include("Class").Include("Student").Where(m=>m.Class.Teacher.TeacherID.Equals(id)).ToList());
        }

        [ActionName("GetExpenses")]
        public IHttpActionResult GetExpenses()
        {

            return Ok(school.Expenses.ToList());
        }

        [ActionName("GetStuds")]
        public IHttpActionResult GetStuds(string id)
        {

           return Ok(school.Students.Include("Class").Where(m=>m.Class.Teacher.TeacherID.Equals(id)).ToList());
        }



        [ActionName("GetAdminSchedule")]
        public IHttpActionResult GetAdminSchedule()
        {

            return Ok(school.Schedules.Include("Class").Include("Teacher").ToList());
        }

        [ActionName("GetTeacherSchedule")]
        public IHttpActionResult GetTeacherSchedule(string id)
        {

            return Ok(school.Schedules.Include("Class").Include("Teacher").Where(m=>m.Teacher.TeacherID.Equals(id)).ToList());
        }

        [Route("api/Demo/Login/{userid}/{pswd}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("Login")]
        public IHttpActionResult Login(string userid,string pswd)
        {
            User u = new User();
            u.UserID = userid;
            u.UserPassword = pswd;
            User u1 = school.Users.FirstOrDefault(m => m.UserID.Equals(u.UserID) && m.UserPassword.Equals(u.UserPassword));
            return Ok(u1);
        }



        [Route("api/Demo/GetTeacherAnnouncements/{userid}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetTeacherAnnouncements")]
        public IHttpActionResult GetTeacherAnnouncements(string userid)
        {
            
            var list = school.Announcements.Where(m=>m.Recepient.Equals("everyone") || m.Recepient.Equals("teacher") || m.Recepient.Equals(userid)).OrderByDescending(m=>m.Date).ToList();
            return Ok(list);
        }


        [Route("api/Demo/GetParentAnnouncements/{userid}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetParentAnnouncements")]
        public IHttpActionResult GetParentAnnouncements(string userid)
        {

            var list = school.Announcements.Where(m => m.Recepient.Equals("everyone") || m.Recepient.Equals("parent") || m.Recepient.Equals(userid)).OrderByDescending(m=>m.Date).ToList();
            return Ok(list);
        }



        [Route("api/Demo/GetTeacherClasses/{userid}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetTeacherClasses")]
        public IHttpActionResult GetTeacherClasses(string userid)
        {

            var list = school.Class.Where(m => m.Teacher.TeacherID.Equals(userid)).ToList();
            return Ok(list);
        }


        [Route("api/Demo/GetDues/{userid}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetDues")]
        public IHttpActionResult GetDues(string userid)
        {

            var list = school.Dues.Include("Student").Where(m => m.Student.Parent.ParentId.Equals(userid)).ToList();
            return Ok(list);
        }


        [Route("api/Demo/GetChildren/{userid}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetChildren")]
        public IHttpActionResult GetChildren(string userid)
        {

            var list = school.Students.Include("Parent").Where(m => m.Parent.ParentId.Equals(userid)).ToList();
            return Ok(list);
        }

        [Route("api/Demo/GetResult/{userid}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetResult")]
        public IHttpActionResult GetResult(string userid)
        {

            var list = school.Results.Include("Student").Where(m => m.Student.StudentID.Equals(userid)).ToList();
            return Ok(list);
        }





        [Route("api/Demo/AddAnnouncement")]
        
        [HttpPost]
        [ActionName("AddAnnouncement")]
        public IHttpActionResult AddAnnouncement([FromBody] MobAnnouncement mb)
        {
            Announcements anc = new Announcements();
            anc.Class = school.Class.FirstOrDefault(m => m.ClassID.Equals(mb.ClassID));
            anc.Message = mb.Message;
            anc.Topic = mb.Topic;
            anc.Recepient = "student";
            anc.Date = System.DateTime.Now;
            

            
            /*
           string[] tkns = (from m in school.Tokens where m.UserID == anc.Recepient || m.UserType == anc.Recepient select m.TokenVal).ToArray();
            PushNotification.send(anc.Message, tkns); */


            school.Announcements.Add(anc);
            school.SaveChanges();

            Notification n = new Notification();
            n.Recepient = anc.Recepient;
            n.SeenBy = "";
            n.Status = "unseen";
            //n.UserID = anc.userID;
            n.Time = DateTime.Now.ToString();
            school.Notifications.Add(n);
            school.SaveChanges();

            return Ok();
        }



        

        [Route("api/Demo/PostToken")]
        [HttpPost]
        [ActionName("PostToken")]
        public IHttpActionResult PostToken([FromBody] Token tk)
        {
            Console.WriteLine("kaaa");
            Token t = new Token();
            t.UserID = tk.UserID;
            t.TokenVal = tk.TokenVal;
            t.UserType = tk.UserType;
            t.TokenKey = t.TokenVal + t.UserID;
            school.Tokens.Add(t);
            school.SaveChanges();
            return Ok("ok");
        }

    }
}
