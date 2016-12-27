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
            var resultList = CreateSearchEngineResultsList("Google");
            //string apiKey = "AIzaSyDAGFKL3kZevjzrFizgnVGnKmZNKUM1hjw";
            //string cx = "007172875963593911035:kpk5tcwf8pa";
            string apiKey = "AIzaSyB8kNz-iLRMinVRviNJHtJUkgPPOAx7mIk";
            string cx = "009511415247016879030:smaostb1cxe";
            var count = 1;
            uint start = 1;
            WebClient webClient = new WebClient();

            try
            {
                for (var i = 0; i < 1; i++)
                {
                    string result = webClient.DownloadString(String.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}&start={3}&alt=json&cr=us", apiKey, cx, query, start));
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Dictionary<string, object> collection = serializer.Deserialize<Dictionary<string, object>>(result);
                    foreach (Dictionary<string, object> item in (IEnumerable)collection["items"])
                    {
                        if (!resultList.Results.Any(r => r.DisplayUrl.Equals(UrlConvert(item["link"].ToString()))))
                        {
                            resultList.Results.Add(new Result
                            {
                                DisplayUrl = UrlConvert(item["link"].ToString()),
                                Title = item["title"].ToString(),
                                Description = item["snippet"].ToString(),
                                Rank = count++
                            });
                        }
                    }
                    start += 10;
                }
            }
            catch (Exception)
            {
                return resultList;
            }
            return resultList;
        }

    }
}
