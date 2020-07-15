using System.Linq;
using System.Web.Mvc;
using TestApp_Domain.Handlers;
using TestApp_MVC.Mapping;
using TestApp_MVC.Models;

namespace TestApp_MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentHandler _studentHandler; 
        public StudentController(IStudentHandler studentHandler)  
        {
            _studentHandler = studentHandler;
        }

        /// <summary>
        /// student/index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new StudentModuleViewModel
            {
                StudentList = _studentHandler.GetStudents().Select(s => s.ToContracts()).ToList(),
                FeaturedStudent = _studentHandler.FeaturedStudent().ToContracts() ?? new StudentViewModel()
            };           
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveStudent(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                _studentHandler.AddStudent(student.ToDomain());
            }
            return RedirectToAction("Index");
        }
    }
}