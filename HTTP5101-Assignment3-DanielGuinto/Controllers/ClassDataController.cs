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
    public class ClassDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Returns a list of Classes in the system
        /// </summary>
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns>
        /// A list of classes
        /// </returns>
        [HttpGet]
        [Route("api/ClassDate/ListClasses/{SearchKey}")]
        public IEnumerable<Class> ListClasses(string SearchKey = null)
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between the web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Classes where (lower(classcode) like lower(@key)) or (lower(classname) like lower(@key))";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");

            //Gather Query result into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Students
            List<Class> Classes = new List<Class> {};

            //Loop Through Each Row of the Result Set
            while (ResultSet.Read())
            {
                //Access Column info by the DB column name as an index
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = (String)ResultSet["classcode"];
                string ClassName = (String)ResultSet["classname"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];

                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;

                //Add the student name to the list
                Classes.Add(NewClass);
            }

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return final list of student names
            return Classes;
        }


        /// <summary>
        /// Returns information of a single class in the system
        /// </summary>
        /// <example>GET api/ClassData/FindClass/{id}</example>
        /// <returns>
        /// Information about Class from database
        /// </returns>
        [HttpGet]
        public Class FindClass(int id)
        {
            Class NewClass = new Class();

            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between the web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Classes left join teachers on classes.teacherid = teachers.teacherid where classid = @id";
            cmd.Parameters.AddWithValue("@id", id);

            //Gather Query result into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop Through Each Row of the Result Set
            while (ResultSet.Read())
            {
                //Access Column info by the DB column name as an index
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = (String)ResultSet["classcode"];
                string ClassName = (String)ResultSet["classname"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];

                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;


                string TeacherFname = Convert.ToString(ResultSet["teacherfname"]);
                string TeacherLname = Convert.ToString(ResultSet["teacherlname"]);

                NewClass.TeacherFname = TeacherFname;
                NewClass.TeacherLname = TeacherLname;
            }

            //Return class information
            return NewClass;
        }


        /// <summary>
        /// Deletes a Class from the database
        /// </summary>
        /// <param name="id"></param>
        /// <example> POST: /api/ClassData/DeleteClass/3 </example>
        [HttpPost]
        public void DeleteClass(int id)
        {

            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from Classes where classid=@id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();

            //Executes non-select statements in MySQL
            cmd.ExecuteNonQuery();

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();
        }

        /// <summary>
        /// Adds a Class to the database
        /// </summary>
        /// <param name="NewClass"></param>
        [HttpPost]
        public void AddClass([FromBody]Class NewClass)
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Insert into classes (classcode, startdate, finishdate, classname) values (@ClassCode, @StartDate, @FinishDate, @ClassName)";
            cmd.Parameters.AddWithValue("@ClassCode", NewClass.ClassCode);
            cmd.Parameters.AddWithValue("@StartDate", NewClass.StartDate);
            cmd.Parameters.AddWithValue("@FinishDate", NewClass.FinishDate);
            cmd.Parameters.AddWithValue("@ClassName", NewClass.ClassName);

            cmd.Prepare();

            //Executes non-select statements in MySQL
            cmd.ExecuteNonQuery();

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();
        }


        public void UpdateClass(int id, [FromBody]Class ClassInfo)
        {
            //Creates an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Opens connection between web server and database
            Conn.Open();

            //Establish a new command for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Update classes set classcode=@ClassCode, startdate=@StartDate, finishdate=@FinishDate, classname=@ClassName where classid=@ClassId";
            cmd.Parameters.AddWithValue("@ClassCode", ClassInfo.ClassCode);
            cmd.Parameters.AddWithValue("@StartDate", ClassInfo.StartDate);
            cmd.Parameters.AddWithValue("@FinishDate", ClassInfo.FinishDate);
            cmd.Parameters.AddWithValue("@ClassName", ClassInfo.ClassName);
            cmd.Parameters.AddWithValue("@ClassId", id);

            cmd.Prepare();

            //Executes non-select statements in MySQL
            cmd.ExecuteNonQuery();

            //Closes connection between the MySQL Database and the WebServer
            Conn.Close();
        }
    }
}
