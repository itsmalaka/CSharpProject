using System;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections.Generic;

namespace RESTServicesJSONParserExample
{
    class Program
    {
        public class Company
        {
            public string Title { get; set; }
            public List<Employee> Employees { get; set; }
        }

        /// <summary>
        /// Employee info
        /// </summary>
        public class Employee
        {
            public string Name { get; set; }
            public EmployeeType EmployeeType { get; set; }
        }

        /// <summary>
        /// Types of employees
        /// (just to se how JSON deals with enums!)
        /// </summary>
        public enum EmployeeType
        {
            CEO,
            Developer
        }

        static void Main(string[] args)
        {
            string url = @"C:\Users\kay\Documents\GitHub\CSharpProject\data.json";
            Console.WriteLine("Hello World!!!");

            Company c = Serializer(url);

            foreach (var v in c.Employees)
            {
                Console.WriteLine(v.Name);
            }
            

            Console.Read();
        }


        static public Company Serializer(string url)
        {
            string json = File.ReadAllText(url);

            Company company = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Company>(json);

            //Company company = new Company();
            return company;
        }
    }
}
