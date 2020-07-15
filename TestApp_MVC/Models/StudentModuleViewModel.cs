using System.Collections.Generic;

namespace TestApp_MVC.Models
{
    public class StudentModuleViewModel
    {
        public StudentViewModel FeaturedStudent { get; set; } = new StudentViewModel();  

        public List<StudentViewModel> StudentList { get; set; } = new List<StudentViewModel>();

        public StudentViewModel NewStudent { get; set; } = new StudentViewModel(); 
    }
}