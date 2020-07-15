using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using TestApp_Repository.Entities;

namespace TestApp_Repository
{
    public interface IStudentRepository
    {
        Student Save(Student student);
        List<Student> Get();
        Student Get(int id);
        Student GetNthHighestPocketMoneyStudent(int n);
    }

    public class StudentRepository : IStudentRepository
    {
        public const string CONNECTION_STRING_KEY = "TestDB";
        public readonly string _connectionString;

        public StudentRepository()
        {
            //read connection string from web.config
            _connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_KEY].ConnectionString;
        }

        /// <summary>
        /// save student data to database
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Student Save(Student student)
        {
            int upsertId = 0;

            //sql script to add student
            string sqlScript = "insert into dbo.student (first_name, last_name, email, pocket_money, password) Values ('" + student.FirstName + "', '" + student.LastName + "', '" + student.Email + "', '" + student.PocketMoney + "', '" + student.Password + "');select cast(scope_identity() as int)";

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sqlCommand = new SqlCommand(sqlScript, sqlConnection);

                //open sql connection
                sqlConnection.Open();

                //exexute sql command
                upsertId = (int)sqlCommand.ExecuteScalar();
            }

            if (upsertId > 0)
              return  Get(upsertId);
            return null;
        }

        /// <summary>
        /// get student list
        /// </summary>
        /// <returns></returns>
        public List<Student> Get()
        {
            var students = new List<Student>();
            //sql script to get all students
            string sqlScript = "select id, first_name, last_name, email, pocket_money, password, is_deleted FROM dbo.student";

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sqlCommand = new SqlCommand(sqlScript, sqlConnection);

                //open sql connection
                sqlConnection.Open();
                
                //exexute sql command
                var sqlDataReader = sqlCommand.ExecuteReader();

                //map data to student model
                students = MapStudentsData(sqlDataReader);
            }

            return students;
        }

        /// <summary>
        /// get student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student Get(int id)
        {
            Student student = null;
            //sql script to get student by id
            string sqlScript = $"select id, first_name, last_name, email, pocket_money, password, is_deleted FROM dbo.student where id={id}";

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sqlCommand = new SqlCommand(sqlScript, sqlConnection);

                //open sql connection
                sqlConnection.Open();

                //exexute sql command
                var sqlDataReader = sqlCommand.ExecuteReader();

                //map data to student model
                student = MapStudentsData(sqlDataReader).FirstOrDefault();
            }

            return student;
        }

        /// <summary>
        /// get student with 2nd highest pocket mokey
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Student GetNthHighestPocketMoneyStudent(int n)
        {
            Student student = null;
            //sql script to get student with nth highest pocket money
            string sqlScript = $"select top 1 T.id, T.first_name, T.last_name, T.email, T.pocket_money, T.password, T.is_deleted from (select top {n} * from [dbo].[student] order by [pocket_money] desc) as T order by T.[pocket_money] asc";

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sqlCommand = new SqlCommand(sqlScript, sqlConnection);

                //open sql connection
                sqlConnection.Open();

                //exexute sql command
                var sqlDataReader = sqlCommand.ExecuteReader();

                //map data to student model
                student = MapStudentsData(sqlDataReader).FirstOrDefault();
            }

            return student ?? new Student();
        }

        /// <summary>
        /// map student data from SqlDataReader to list of student
        /// </summary>
        /// <param name="sqlDataReader"></param>
        /// <returns></returns>
        private List<Student> MapStudentsData(SqlDataReader sqlDataReader)
        {
            List<Student> students = new List<Student>();

            //if sqldatareader has data
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //map data sqldatareader row to student model 
                    var student = new Student
                    {
                        Id = sqlDataReader.GetInt32(0),
                        FirstName = sqlDataReader.GetString(1),
                        LastName = sqlDataReader.GetString(2),
                        Email = sqlDataReader.GetString(3),
                        PocketMoney = sqlDataReader.GetInt32(4),
                        Password = sqlDataReader.GetString(5),
                        IsDeleted = sqlDataReader.GetByte(6) > 0 ? true : false
                    };

                    students.Add(student);
                }
            }

            return students;
        }
    }
}
