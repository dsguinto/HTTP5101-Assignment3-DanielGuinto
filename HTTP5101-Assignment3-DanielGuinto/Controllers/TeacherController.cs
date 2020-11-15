using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP5101_Assignment3_DanielGuinto.Models;

namespace HTTP5101_Assignment3_DanielGuinto.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teachers/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeacher(SearchKey);

            return View(Teachers);
        }

        // GET: /Teachers/Show{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            //IEnumerable<Course> NewCourse = controller.ListCourses(id);

            return View(NewTeacher);
        }
    }
}