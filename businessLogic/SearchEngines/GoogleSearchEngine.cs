using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;
using Google.Apis.Services;
using System.Net;
using System.Web.Script.Serialization;
using System.Collections;

namespace businessLogic.SearchEngines
{
    internal class GoogleSearchEngine : BaseSearchEngine, ISearchEngine
    {
        private static string ApiKey = "AIzaSyDAGFKL3kZevjzrFizgnVGnKmZNKUM1hjw";
        private static string SearchEngineId = "007172875963593911035:kpk5tcwf8pa";

        public GoogleSearchEngine() 
        {
           
        }

        public SearchEngineResultsList Search(string query)
        {
            var resultList = new SearchEngineResultsList
            {
                SearchEngineName = "Google",
                Results = new List<Result>()
            };
            WebClient webClient = new WebClient();
            //string apiKey = "AIzaSyBpBXE1xDbLr5JDDzpUUAhTJ4UYAKFxsWM";
            string apiKey = "AIzaSyDAGFKL3kZevjzrFizgnVGnKmZNKUM1hjw";
            string cx = "007172875963593911035:kpk5tcwf8pa";
            int count = 1;
            uint start = 0;
            string result = webClient.DownloadString(String.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}&start={3}&alt=json", apiKey, cx, query, start));
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> collection = serializer.Deserialize<Dictionary<string, object>>(result);
            foreach (Dictionary<string, object> item in (IEnumerable)collection["items"])
            {
                new Result
                {
                    Link = item["link"].ToString(),
                    Title = item["title"].ToString(),
                    Description = item["description"].ToString(),
                    Rank = count++
                    
                };
                
                //Console.WriteLine("Title: {0}", item2["title"]);
                //Console.WriteLine("Link: {0}", item2["link"]);
                //Console.WriteLine();
                ////   title1.Text = item["title"].ToString();
                ////    link1.Text = item["link"].ToString();
                //TextBox1.Text += count + "\n" + item2["title"].ToString();
                //TextBox1.Text += "\n";
                //TextBox1.Text += item2["link"].ToString();
                //TextBox1.Text += "\n";
                //count++;

            }
            return resultList;
        }
    }
}
