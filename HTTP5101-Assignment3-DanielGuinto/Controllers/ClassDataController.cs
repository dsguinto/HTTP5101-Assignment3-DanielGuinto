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
            cmd.CommandText = "Select * from Classes inner join teachers on classes.teacherid = teachers.teacherid where classid = " + id;
            //cmd.CommandText = "Select * from Classes where classid = " + id;

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


                string TeacherFname = (String)ResultSet["teacherfname"];
                string TeacherLname = (String)ResultSet["teacherlname"];

                NewClass.TeacherFname = TeacherFname;
                NewClass.TeacherLname = TeacherLname;
            }

            //Return class information
            return NewClass;
        }
    }
}
