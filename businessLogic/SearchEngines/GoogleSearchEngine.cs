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
        //private const string ApiKey = "AIzaSyDAGFKL3kZevjzrFizgnVGnKmZNKUM1hjw";
        //private const string SearchEngineId = "007172875963593911035:kpk5tcwf8pa";

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
            //string apiKey = "AIzaSyDAGFKL3kZevjzrFizgnVGnKmZNKUM1hjw";
            //string cx = "007172875963593911035:kpk5tcwf8pa";

            string apiKey = "AIzaSyB8kNz-iLRMinVRviNJHtJUkgPPOAx7mIk";
            string cx = "009511415247016879030:smaostb1cxe";

            int count = 1;
            uint start = 1;

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    string result = webClient.DownloadString(String.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}&start={3}&alt=json", apiKey, cx, query, start));
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Dictionary<string, object> collection = serializer.Deserialize<Dictionary<string, object>>(result);
                    foreach (Dictionary<string, object> item in (IEnumerable)collection["items"])
                    {
                        resultList.Results.Add(new Result
                        {
                            Link = item["link"].ToString().Replace("https://","").Replace("http://", "").Replace("www.", ""),
                            Title = item["title"].ToString(),
                            Description = item["snippet"].ToString(),
                            Rank = count++
          
                        });

                    }
                    start += 10;
                }
            }
            catch (Exception e)
            {
                var x = e.Message;
                return resultList;

            }
            return resultList;
        } 

    }
}
