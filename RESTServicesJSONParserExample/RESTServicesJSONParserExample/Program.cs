using System;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

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

        public class Weather
        {
            public Coord Coord { get; set; }
        }


        public class Coord
        {
            public string lon { get; set; }
            public string lat { get; set; }
        }


        static void Main(string[] args)
        {
            //string url = @"C:\Users\aa\Documents\GitHub\CSharpProject\data.json";
            string url = @"http://api.openweathermap.org/data/2.5/weather?q=minneapolis";
            Console.WriteLine("Hello World!!!");

            Company c = SerializerJson(url);

            SerializerXML("https://news.ycombinator.com/rss");

            foreach (var v in c.Employees)
            {
                Console.WriteLine(v.Name);
            }
            

            Console.Read();
        }

        public static string GetContent(string url)
        {
            String responseString;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                // process the response here
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();

                }
                return responseString;
            }
        }

        static public Company SerializerJson(string url)
        {
            string str = GetContent(url);
                var obj = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Weather>(str);
                
            Company company = new Company();
            return company;
        }

        static void SerializerXML(string url)
        {
            string str = GetContent(url);
            //str = str.Replace("<rss version=\"2.0\">", "").Replace("<rss>","");
            StringReader reader = new StringReader(str);
            XmlSerializer serializer = new XmlSerializer(typeof(rss));
            rss r =  (rss)serializer.Deserialize(reader);

            foreach (var item in r.channel[0].item)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine(item.title);
                Console.WriteLine("\t" + item.link);
            }
            Console.Read();
        }
    }

}
