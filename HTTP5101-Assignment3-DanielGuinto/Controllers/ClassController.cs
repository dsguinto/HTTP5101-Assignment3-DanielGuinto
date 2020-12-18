using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP5101_Assignment3_DanielGuinto.Models;

namespace HTTP5101_Assignment3_DanielGuinto.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        // GET: Class/List
        public ActionResult List(string SearchKey = null)
        {
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ListClasses(SearchKey);
             
            return View(Classes);
        }

        // GET: Class/Show/{id}
        public ActionResult Show(int id)
        {
            ClassDataController controller = new ClassDataController();
            Class SelectedClass = controller.FindClass(id);

            return View(SelectedClass);
        }

        // GET: /Class/DeleteConfirm/{id}
        public ActionResult ConfirmDelete(int id)
        {
            ClassDataController controller = new ClassDataController();
            Class NewClass = controller.FindClass(id);

            return View(NewClass);
        }

        //POST : /Class/Delete/{id}
        public ActionResult Delete(int id)
        {
            ClassDataController controller = new ClassDataController();
            controller.DeleteClass(id);
            return RedirectToAction("List");
        }


        // GET : /Class/Add
        public ActionResult Add()
        {
            return View();
        }

        //POST: /Class/Create
        [HttpPost]
        public ActionResult Create(string ClassCode, string ClassName, DateTime StartDate, DateTime FinishDate)
        {

            Class NewClass = new Class();
            NewClass.ClassCode = ClassCode;
            NewClass.ClassName = ClassName;
            NewClass.StartDate = StartDate;
            NewClass.FinishDate = FinishDate;

            ClassDataController controller = new ClassDataController();
            controller.AddClass(NewClass);

            return RedirectToAction("List");
        }

        //GET : /Class/Update
        public ActionResult Update(int id)
        {
            ClassDataController controller = new ClassDataController();
            Class SelectedClass = controller.FindClass(id);

            return View(SelectedClass);
        }

        //POST : /Class/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string ClassCode, string ClassName, DateTime StartDate, DateTime FinishDate)
        {

            Class ClassInfo = new Class();
            ClassInfo.ClassCode = ClassCode;
            ClassInfo.ClassName = ClassName;
            ClassInfo.StartDate = StartDate;
            ClassInfo.FinishDate = FinishDate;


            ClassDataController controller = new ClassDataController();
            controller.UpdateClass(id, ClassInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}