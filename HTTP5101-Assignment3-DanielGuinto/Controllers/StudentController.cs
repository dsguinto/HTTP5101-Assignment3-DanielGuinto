using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP5101_Assignment3_DanielGuinto.Models;

namespace HTTP5101_Assignment3_DanielGuinto.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/List
        public ActionResult List(string SearchKey = null)
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);

            return View(Students);
        }

        // GET: /Student/Show{id}
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }

        // GET: /Student/DeleteConfirm/{id}
        public ActionResult ConfirmDelete(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }

        //POST : /Student/Delete/{id}
        public ActionResult Delete(int id)
        {
            StudentDataController controller = new StudentDataController();
            controller.DeleteStudent(id);
            return RedirectToAction("List");
        }


        // GET : /Student/Add
        public ActionResult Add()
        {
            return View();
        }

        //POST: /Student/Create
        [HttpPost]
        public ActionResult Create(string StudentFname, string StudentLname, string StudentNumber)
        {

            Student NewStudent = new Student();
            NewStudent.StudentFname = StudentFname;
            NewStudent.StudentLname = StudentLname;
            NewStudent.StudentNumber = StudentNumber;

            StudentDataController controller = new StudentDataController();
            controller.AddStudent(NewStudent);

            return RedirectToAction("List");
        }
    }
}