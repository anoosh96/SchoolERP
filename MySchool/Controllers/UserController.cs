using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySchool.Models;
using System.Data.Entity;

namespace MySchool.Controllers
{
    public class UserController : Controller
    {
        private SchoolContext school = new SchoolContext();
        // GET: User
        public ActionResult Index()
        {
            /*if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }*/
            return View("AdminHome");
        }

        [HttpGet]
        public ActionResult AddStudent()
        {
            /*if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }*/
            Student s = new Student();
            s.Parent = new Parent();
            s.Class = new Class_();
            TempData["status"] = "adding";
            return View(s);
            
        }


        


        [HttpPost]
        public ActionResult AddStudent(FormCollection frm)
        {

            Student std = new Student();
            Parent prnt = new Parent();
            User usr1 = new User();
            User usr2 = new User();
            Class_ cls = new Class_();
            bool val = false;
            //ModelState.Clear();
            //SchoolContext school = new SchoolContext();
            try
            {

                   
                
                    //creating Parent
                    prnt.ParentName = Convert.ToString(frm["ParentName"]);
                    prnt.Address = Convert.ToString(frm["Address"]);
                    prnt.Contact = Convert.ToString(frm["Contact"]);
                    prnt.ParentId = Convert.ToString(frm["ParentId"]);
                
                   
                    
                

                std.FirstName = Convert.ToString(frm["FirstName"]);
                std.LastName = Convert.ToString(frm["LastName"]);
                //std.Class.ClassID = Convert.ToString(frm["Class"]);
                string classid = Convert.ToString(frm["ClassID"]);
                classid += Convert.ToString(frm["Section"]);
                std.Class = school.Class.FirstOrDefault(m => m.ClassID.Equals(classid));

                std.Class.Section = Convert.ToString(frm["Section"]); ;
                std.Gender = Convert.ToString(frm["Gender"]);
                std.Dob = Convert.ToDateTime(frm["Dob"]);
                DateTime now = DateTime.Today;

                DateTime bday = std.Dob.Value;
                int age = now.Year - bday.Year;
                if (bday > now.AddYears(-age)) age--;

                switch (std.Class.ClassID)
                {
                    case "1A":
                    case "1B":
                        if (age < 6 || age > 8)
                        {
                            ModelState.AddModelError("Dob", "Age Limit Violated");
                        }
                        break;


                    case "2A":
                    case "2B":
                        if (age < 7 || age > 9)
                        {
                            ModelState.AddModelError("Dob", "Age Limit Violated");
                        }
                        break;

                    case "3A":
                    case "3B":
                        if (age < 8 || age > 10)
                        {
                            ModelState.AddModelError("Dob", "Age Limit Violated");
                        }
                        break;

                    case "4A":
                    case "4B":
                        if (age < 9 || age > 11)
                        {
                            ModelState.AddModelError("Dob", "Age Limit Violated");
                        }
                        break;

                    case "6A":
                    case "6B":
                        if (age < 10 || age > 12)
                        {
                            ModelState.AddModelError("Dob", "Age Limit Violated");
                        }
                        break;

                    case "7A":
                    case "7B":
                        if (age < 11 || age > 13)
                        {
                            ModelState.AddModelError("Dob", "Age Limit Violated");
                        }
                        break;

                }


                std.StudentID = Convert.ToString(frm["StudentId"]);
                std.City = Convert.ToString(frm["City"]);
                

                
                Student s1 = school.Students.Find(std.StudentID);
                Parent p = school.Parents.Find(prnt.ParentId);
                if (s1 != null)
                {
                    ModelState.AddModelError("StudentID", "A Student with Same ID Already Exists");
                }

                if (p != null && (p.ParentName.Equals(prnt.ParentName) || p.Contact.Equals(prnt.Contact) || p.Address.Equals(prnt.Address)))
                {

                    std.Parent = p;
                }
                else if (p != null && (!p.ParentName.Equals(prnt.ParentName) || !p.Contact.Equals(prnt.Contact) || !p.Address.Equals(prnt.Address)))
                {
                    std.Parent = p;
                    ModelState.AddModelError("Parent.ParentId", "A parent with same id already exists");
                }
                else if (p == null)
                {
                    val = true;
                    std.Parent = prnt;
                }
                
                TryUpdateModel(std);
                //ViewBag.Message = s;
                if (ModelState.IsValid)
                {
                    TempData["add"] = "added";
                    
                    std.Class.Strength = std.Class.Strength + 1;

                    if (val)
                    {
                        school.Parents.Add(prnt);
                        school.SaveChanges();
                    }
                    
                    school.Students.Add(std);
                    school.SaveChanges();
                }
                else
                {
                    return View(std);
                }





                //creating user for Student

                usr1.UserID = Convert.ToString(frm["StudentId"]);
                usr1.UserType = "student";
                usr1.UserPassword = "123";  //default password

                school.Users.Add(usr1);
                school.SaveChanges();

                //creating user for Parent
                
                    usr2.UserID = Convert.ToString(frm["ParentId"]);
                    usr2.UserType = "parent";
                    usr2.UserPassword = "123";  //default password
                    if (school.Users.Find(usr2.UserID) == null)
                    {
                        

                        school.Users.Add(usr2);

                        school.SaveChanges();

                    }
                
                
                return RedirectToAction("AddStudent");
            }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null)
                      ? ex.InnerException.Message
                      : "";
                ViewBag.Message = innerMessage;
                return View("ErrorView");
            }
            
        }

        [HttpGet]
        public ActionResult AddTeacher()
        {
            Teacher t = new Teacher();
            return View("AddTeacher",t);
        }


        [HttpPost]
        public ActionResult AddTeacher(FormCollection frm)
        {

            Teacher teacher = new Teacher();
            teacher.Name = Convert.ToString(frm["Name"]);
            teacher.TeacherID = Convert.ToString(frm["TeacherId"]);
            teacher.Gender = Convert.ToString(frm["Gender"]);
            teacher.City = Convert.ToString(frm["City"]);

            User u = new User();
            u.UserID = Convert.ToString(frm["TeacherId"]);
            u.UserPassword = "123";
            u.UserType = "teacher";

            
                TryUpdateModel(teacher);
                Teacher t = school.Teachers.Find(teacher.TeacherID);
            if (t != null)
            {
                ModelState.AddModelError("TeacherId", "Teacher with same ID Exists");
            }
                if (ModelState.IsValid)
                {
                    school.Teachers.Add(teacher);
                    school.Users.Add(u);
                    school.SaveChanges();
                    return RedirectToAction("index");
                }
                else
                {
                    return View(teacher);
                }
            }



        public ActionResult ShowTeachers()
        {
            /*if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }*/
            try
            {
                return View();

            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.InnerException.ToString();
                return View("ErrorView");
            }
        }

        public ActionResult EditTeacher(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            

            Teacher t = school.Teachers.Find(id);
            if (t == null)
            {
                return HttpNotFound();
            }
            return View(t);
        }

        [HttpPost]
        public ActionResult EditTeacher(Teacher t)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (ModelState.IsValid)
            {
                school.Entry(t).State = EntityState.Modified;

                school.SaveChanges();
                TempData["status"] = "edited";
                return RedirectToAction("ShowTeachers");
            }
           
            return View(t);
        }



        public ActionResult Details(string id)
        {
            return PartialView(school.Students.Include("Parent").Include("Class").FirstOrDefault(m => m.StudentID.Equals(id)));
        }



        public ActionResult ShowStudents()
        {
            /*if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }*/
            
                return View();

            
           
        }

        public ActionResult ShowParents()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            try
            {
                return View(school.Parents.ToList());

            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.InnerException.ToString();
                return View("ErrorView");
            }
        }



        [HttpGet]
        public ActionResult EditStudent(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            Student s = school.Students.Include("Class").Include("Parent").FirstOrDefault(m => m.StudentID.Equals(id));
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
          
        }

        [HttpPost]

        public ActionResult EditStudent(FormCollection frm)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Parent prnt = new Parent();
            //Student std = new Student();

            string stdid = Convert.ToString(frm["StudentID"]);
            Student std = school.Students.Find(stdid);
            string prntid = Convert.ToString(frm["Parent.ParentId"]);
            std.Parent = school.Parents.FirstOrDefault(m => m.ParentId.Equals(prntid));

            std.StudentID = Convert.ToString(frm["StudentID"]);
            std.FirstName = Convert.ToString(frm["FirstName"]);
            std.LastName = Convert.ToString(frm["LastName"]);
            //std.Class.ClassID = Convert.ToString(frm["Class"]);
            string classid = Convert.ToString(frm["ClassID"])+ Convert.ToString(frm["Section"]);

            std.Class = school.Class.FirstOrDefault(m => m.ClassID.Equals(classid));

            std.Gender = Convert.ToString(frm["Gender"]);
            std.Dob = Convert.ToDateTime(frm["Dob"]);
            //std.StudentID = Convert.ToString(frm["StudentId"]);
            std.City = Convert.ToString(frm["City"]);
            //std.Parent = pr
            TryUpdateModel(std);
            if (ModelState.IsValid)
            {
                school.SaveChanges();
                TempData["edit"] = "ok";
                return RedirectToAction("ShowStudents");
            }
            return View(std);
            
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            Student s = school.Students.FirstOrDefault(m => m.StudentID.Equals(id));

            return View(s);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(string id)
        {
            Student s = school.Students.Find(id);
            school.Students.Remove(s);
            school.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult EditParent(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            Parent s = school.Parents.Find(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);

        }

        [HttpPost]
        public ActionResult EditParent(FormCollection frm)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string id = frm["ParentId"].ToString();
            Parent prnt = school.Parents.Find(id);
            //Student std = new Student();

            

            prnt.Address = Convert.ToString(frm["Address"]);
            prnt.Contact = Convert.ToString(frm["Contact"]);
            prnt.ParentName = Convert.ToString(frm["ParentName"]);
            //std.Class.ClassID = Convert.ToString(frm["Class"]);
            TryUpdateModel(prnt);
            //std.Parent = pr
            if (ModelState.IsValid) {
                school.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(prnt);

        }


        public ActionResult AdminHome()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View("AdminHome");
        }

        public ActionResult getTeacherClasses(string id)
        {
            var list = (from n in school.Class where n.Teacher.TeacherID.Equals(id) select new {n.ClassID }).ToList();
           
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult getTeacherClass(string classid)
        {
            var list = (from n in school.Students where n.Class.ClassID.Equals(classid) select new { n.StudentID,n.FirstName }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public ActionResult getTeacherSubjects(string teacherid, string clasid)
        {
            var list = (from n in school.Schedules where (n.Teacher.TeacherID.Equals(teacherid) && n.Class.ClassID.Equals(clasid))  select new { n.Subject }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TeacherHome()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        public ActionResult ParentHome()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }



        public ActionResult StudentHome(string id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Student l = new Student();
            try
            {
                l = school.Students.Include("Class").Include("Parent").FirstOrDefault(m => m.StudentID.Equals(id));
                if (l != null)
                {
                    return View(l);
                }
                else
                {
                    ViewBag.Message = id;
                    return View("ErrorView");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.InnerException.ToString();
                return View("ErrorView");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User log)
        {

            //IEnumerable<Login> obj = from n in context.Logins where n.Username.Equals(log.Username) select n;

            User l = new User();
            l = school.Users.FirstOrDefault(m => m.UserID.Equals(log.UserID));
            if (l != null)
            {
                int stds = school.Students.Count();
                int teachers = school.Teachers.Count();
                int parents = school.Parents.Count();

                TempData["stds"] = stds;
                TempData["teachers"] = teachers;
                TempData["parents"] = parents;

                if (l.UserPassword.Equals(log.UserPassword))
                {
                    if (l.UserType.Equals("admin"))
                    {
                        Session["username"] = l.UserID;
                        Session.Timeout = 3;
                        return RedirectToAction("Index","User");
                    }
                    else if (l.UserType.Equals("student"))
                    {
                        Session["username"] = l.UserID;
                        Session.Timeout = 10;
                        return RedirectToAction("StudentHome", "User",new { id = l.UserID });
                    }

                    else if (l.UserType.Equals("teacher"))
                    {
                        Session["username"] = l.UserID;
                        Session.Timeout = 10;
                        return RedirectToAction("TeacherHome", "User", new { id = l.UserID });
                    }

                    else if (l.UserType.Equals("parent"))
                    {
                        Session["username"] = l.UserID;
                        Session.Timeout = 10;
                        return RedirectToAction("ParentHome", "User", new { id = l.UserID });
                    }



                }
                ViewBag.Message = "Invalid Username Or Password";
                return View(log);

            }
            ViewBag.Message = "Invalid Username Or Password";
            return View(log);

        }




        [HttpGet]
        public ActionResult Login1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login1(User log)
        {

            //IEnumerable<Login> obj = from n in context.Logins where n.Username.Equals(log.Username) select n;

            User l = new User();
            l = school.Users.FirstOrDefault(m => m.UserID.Equals(log.UserID));
            if (l != null)
            {
                int stds = school.Students.Count();
                int teachers = school.Teachers.Count();
                int parents = school.Parents.Count();

                TempData["stds"] = stds;
                TempData["teachers"] = teachers;
                TempData["parents"] = parents;

                if (l.UserPassword.Equals(log.UserPassword))
                {
                    if (l.UserType.Equals("admin"))
                    {
                        Session["username"] = l.UserID;
                        Session.Timeout = 3;
                        return RedirectToAction("Index", "User");
                    }
                    else if (l.UserType.Equals("student"))
                    {
                        Session["username"] = l.UserID;
                        Session.Timeout = 10;
                        return RedirectToAction("StudentHome", "User", new { id = l.UserID });
                    }

                    else if (l.UserType.Equals("teacher"))
                    {
                        Session["username"] = l.UserID;
                        Session.Timeout = 10;
                        return RedirectToAction("TeacherHome", "User", new { id = l.UserID });
                    }

                    else if (l.UserType.Equals("parent"))
                    {
                        Session["username"] = l.UserID;
                        Session.Timeout = 10;
                        return RedirectToAction("ParentHome", "User", new { id = l.UserID });
                    }



                }
                ViewBag.Message = "Invalid Username Or Password";
                return View(log);

            }
            ViewBag.Message = "Invalid Username Or Password";
            return View(log);

        }










        public ActionResult search()
        {
            return View();
        }

        public ActionResult searchStudents()
        {
            return View();
        }
    }
}