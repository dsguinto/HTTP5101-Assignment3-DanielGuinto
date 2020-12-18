using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTP5101_Assignment3_DanielGuinto.Models;
using MySql.Data.MySqlClient;

namespace HTTP5101_Assignment3_DanielGuinto.Controllers
{
    public class StudentDataController : ApiController
    {
        //The database context class which allows us to acces our MySQL Database
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudent</example>
        /// <returns>
        /// A list of students
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeacher/{SearchKey}")]
        public IEnumerable<Student> ListStudents(string SearchKey = null)
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between the web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from students where (lower(studentfname) like lower(@key)) OR (lower(studentlname) like lower(@key)) or (lower(concat(studentfname,' ', studentlname)) like lower(@key)) or (studentnumber like @key) or (enroldate like @key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");

            //Gather Query result into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Students
            List<Student> Students = new List<Student> { };

            //Loop Through Each Row of the Result Set
            while (ResultSet.Read())
            {
                //Access Column info by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = (String)ResultSet["studentfname"];
                string StudentLname = (String)ResultSet["studentlname"];
                string StudentNumber = (String)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate.Date;

                //Add the student name to the list
                Students.Add(NewStudent);
            }

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return final list of student names
            return Students;
        }


        /// <summary>
        /// Returns information of a single Student in the system
        /// </summary>
        /// <example>GET api/StudentData/FindStudent/{id}</example>
        /// <returns>
        /// Information about Student from database
        /// </returns>
        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between the web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from students where studentid = @id";
            cmd.Parameters.AddWithValue("@id", id);


            //Gather Query result into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop Through Each Row of the Result Set
            while (ResultSet.Read())
            {
                //Access Column info by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = (String)ResultSet["studentfname"];
                string StudentLname = (String)ResultSet["studentlname"];
                string StudentNumber = (String)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate.Date;
            }
            //Return student information
            return NewStudent;
        }


        /// <summary>
        /// Deletes a Student from the database
        /// </summary>
        /// <param name="id"></param>
        /// <example> POST: /api/StudentData/DeleteStudent/3 </example>
        [HttpPost]
        public void DeleteStudent(int id)
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from Students where studentid = @id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();

            //Executes non-select statements in MySQL
            cmd.ExecuteNonQuery();

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();
        }

        /// <summary>
        /// Adds a Student to the database
        /// </summary>
        /// <param name="NewStudent"></param>
        [HttpPost]
        public void AddStudent([FromBody] Student NewStudent)
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Insert into students (studentfname, studentlname, studentnumber, enroldate) values (@StudentFname, @StudentLname, @StudentNumber, CURRENT_DATE())";
            cmd.Parameters.AddWithValue("@StudentFname", NewStudent.StudentFname);
            cmd.Parameters.AddWithValue("@StudentLname", NewStudent.StudentLname);
            cmd.Parameters.AddWithValue("@StudentNumber", NewStudent.StudentNumber);

            cmd.Prepare();

            //Executes non-select statements in MySQL
            cmd.ExecuteNonQuery();

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();
        }

        public void UpdateStudent(int id, [FromBody]Student StudentInfo)
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();


            //SQL Query
            cmd.CommandText = "Update students set studentfname=@StudentFname, studentlname=@StudentLname, studentnumber=@StudentNumber, enroldate=@EnrolDate where studentid=@StudentId";
            cmd.Parameters.AddWithValue("@StudentFname", StudentInfo.StudentFname);
            cmd.Parameters.AddWithValue("@StudentLname", StudentInfo.StudentLname);
            cmd.Parameters.AddWithValue("@StudentNumber", StudentInfo.StudentNumber);
            cmd.Parameters.AddWithValue("@EnrolDate", StudentInfo.EnrolDate);
            cmd.Parameters.AddWithValue("@StudentId", id);

            cmd.Prepare();

            //Executes non-select statements in MySQL
            cmd.ExecuteNonQuery();

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();
        }
    }
}
