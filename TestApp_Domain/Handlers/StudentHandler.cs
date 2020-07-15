using System;
using System.Collections.Generic;
using System.Linq;
using TestApp_Domain.Mappings;
using TestApp_Domain.Models;
using TestApp_Domain.Utilities;
using TestApp_Repository;

namespace TestApp_Domain.Handlers
{
    public interface IStudentHandler
    {
        Student GetStudentById(int id);
        List<Student> GetStudents();
        Student AddStudent(Student student);
        Student UpdateStudent(Student student);
        Student FeaturedStudent();
    }

    public class StudentHandler : IStudentHandler
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger _logger;

        public StudentHandler(IStudentRepository studentRepository, ILogger logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        /// <summary>
        /// get student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student GetStudentById(int id)
        {
            Student student = null;

            try
            {
                student = _studentRepository.Get(id).ToDomain();
            }
            catch (Exception ex)
            {
                _logger.Log("GetStudentById failed", ex);
            }

            return student;
        }

        /// <summary>
        /// get student list
        /// </summary>
        /// <returns></returns>
        public List<Student> GetStudents()
        {
            var students = new List<Student>();

            try
            {
                students = _studentRepository.Get().Select(s => s.ToDomain()).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log("GetStudents failed", ex);
            }

            return students;
        }
        public Student AddStudent(Student student)
        {
            Student createdStudent = null;

            try
            {
                var stud = _studentRepository.Save(student.ToRepo());

                //map repo model to domain
                createdStudent = stud.ToDomain();
            }
            catch (Exception ex)
            {
                _logger.Log($"AddStudent {student.FirstName} failed", ex);
            }

            return createdStudent;
        }

        /// <summary>
        /// update student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Student UpdateStudent(Student student)
        {            
            Student updatedStudent = null;

            try
            {
                var stud = _studentRepository.Save(student.ToRepo());
                updatedStudent = stud.ToDomain();
            }
            catch (Exception ex)
            {
                _logger.Log($"UpdateStudent {student.FirstName} failed", ex);
            }

            return updatedStudent;
        }

        /// <summary>
        /// get featured student
        /// </summary>
        /// <returns></returns>
        public Student FeaturedStudent()
        {
            Student featuredStudent = null;

            try
            {
                //get student with 2nd highest pocket money
                featuredStudent = _studentRepository.GetNthHighestPocketMoneyStudent(2).ToDomain();
            }
            catch (Exception ex)
            {
                _logger.Log("FeaturedStudent failed", ex);
            }

            return featuredStudent;
        }
    }
}
