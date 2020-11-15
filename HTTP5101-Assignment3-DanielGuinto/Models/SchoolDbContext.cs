using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace HTTP5101_Assignment3_DanielGuinto.Models
{
    public class SchoolDbContext
    {
        private static string User {  get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }


        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                + "; user = " + User
                + "; database = " + Database
                + "; port = " + Port
                + "; password = " + Password;
            }
        }
        /// <summary>
        /// Returns a connetion to the blog database
        /// </summary>
        /// <example>
        /// private Teacher School = new Teacher();
        /// MySqlConnection Conn = Blog.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            //Instantiating the MySQLConnection Class to create an object
            //The object is a specific connection to our blog database on port 3306 of localhost
            return new MySqlConnection(ConnectionString); 
        }
    }
}