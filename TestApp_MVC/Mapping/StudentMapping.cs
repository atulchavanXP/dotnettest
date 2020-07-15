using mvc = TestApp_MVC.Models;
using domain = TestApp_Domain.Models;
using AutoMapper;

namespace TestApp_MVC.Mapping
{
    public class StudentMapping: Profile
    {
        public StudentMapping()
        {
            CreateMap<mvc.StudentViewModel, domain.Student>();
            CreateMap<domain.Student, mvc.StudentViewModel>();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }

    public static class StudentMappingExtensions
    {
        public static domain.Student ToDomain(this mvc.StudentViewModel source)
        {
            return Mapper.Map<domain.Student>(source);
        }

        public static mvc.StudentViewModel ToContracts(this domain.Student source)
        {
            return Mapper.Map<mvc.StudentViewModel>(source);
        }
    }
}