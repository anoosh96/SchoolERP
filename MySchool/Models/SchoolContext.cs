using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MySchool.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("SchoolContext")
            {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SchoolContext, MySchool.Migrations.Configuration>());
        }

       
       public DbSet<MySchool.Models.User> Users { get; set; }

       public DbSet<MySchool.Models.Student> Students { get; set; }


       public DbSet<MySchool.Models.Parent> Parents { get; set; }

        public DbSet<MySchool.Models.Attendance> Attendance { get; set; }

        public DbSet<MySchool.Models.Class_> Class { get; set; }


        public DbSet<MySchool.Models.Result> Results { get; set; }

        public DbSet<MySchool.Models.Teacher> Teachers { get; set; }


        public DbSet<MySchool.Models.Announcements> Announcements { get; set; }

        public DbSet<MySchool.Models.Schedule> Schedules { get; set; }

        public DbSet<MySchool.Models.Due> Dues { get; set; }

        public DbSet<MySchool.Models.Expense> Expenses { get; set; }


        public DbSet<MySchool.Models.Notification> Notifications { get; set; }

        public DbSet<MySchool.Models.Token> Tokens { get; set; }

    }
}