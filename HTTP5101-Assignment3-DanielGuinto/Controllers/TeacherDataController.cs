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
    public class TeacherDataController : ApiController
    {
        //The database context class which allows us to acces our MySQL Database
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeacher</example>
        /// <returns>
        /// A list of teachers 
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeacher/{SearchKey}")]
        public IEnumerable<Teacher> ListTeacher(string SearchKey=null)
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from teachers where (lower(teacherfname) like lower(@key)) OR (lower(teacherlname) like lower(@key)) or (lower(concat(teacherfname,' ', teacherlname)) like lower(@key)) or (salary like @key) or (hiredate like @key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");

            //Gather Query result into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Creates an empty list of Teacher
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row of the Result Set
            while (ResultSet.Read())
            {
                //Access Column info by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (String)ResultSet["teacherfname"];
                string TeacherLname = (String)ResultSet["teacherlname"];
                string EmployeeNumber = (String)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Add the teacher name to the list
                Teachers.Add(NewTeacher);
            }

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return final list of teacher names
            return Teachers;
        }

        /// <summary>
        /// Returns information of a single Teacher in the system
        /// </summary>
        /// <example>GET api/TeacherData/FindTeacher/{id}</example>
        /// <returns>
        /// Information about Teacher from database
        /// </returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from teachers left join classes on teachers.teacherid = classes.teacherid where teachers.teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);

            //Gather Query result into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Courses = new List<Teacher> { };

            //Loop Through Each Row of the Result Set
            while (ResultSet.Read())
            {
                //Access Column info by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (String)ResultSet["teacherfname"];
                string TeacherLname = (String)ResultSet["teacherlname"];
                string EmployeeNumber = (String)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;


                //Access the Classes DB information through the inner join
                //Note: Unsure how to make multiple classes appear for teachers. This method only returns 1 course.
                string ClassCode = Convert.ToString(ResultSet["classcode"]);
                string ClassName = Convert.ToString(ResultSet["classname"]);
                NewTeacher.ClassCode = ClassCode;
                NewTeacher.ClassName = ClassName;

            }
            return NewTeacher;
        }


        /// <summary>
        /// Deletes a teacher from the database
        /// </summary>
        /// <param name="id"></param>
        /// <example> POST: /api/TeacherData/DeleteTeacher/3 </example>
        
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Executes non-select statements in MySQL
            cmd.ExecuteNonQuery();

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();
        }

        /// <summary>
        /// Adds a teacher to the database
        /// </summary>
        /// <param name="NewTeacher"></param>
        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname, @TeacherLname, @EmployeeNumber, CURRENT_DATE(), @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            cmd.Prepare();

            //Executes non-select statements in MySQL
            cmd.ExecuteNonQuery();

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();
        }
    }
}
