using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("https://random-word-api.herokuapp.com/word");
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = "application/json";
            string word;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                word = sr.ReadToEnd();
            }
            word = word.Substring(2, word.Length - 4);
            Console.WriteLine(word);
            Console.ReadKey();

        }
    }
}
