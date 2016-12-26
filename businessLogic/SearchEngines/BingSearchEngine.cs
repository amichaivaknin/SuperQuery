using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;
using System.Web.Script.Serialization;

namespace businessLogic.SearchEngines
{
    class BingSearchEngine : BaseSearchEngine, ISearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = new SearchEngineResultsList
            {
                SearchEngineName = "Bing",
                Results = new List<Result>()
            };
            string apiKey = "364dc7e685ed4672b4fa87ad9980d454";
            int count = 1;
            int start = 0;

            for (int i = 0; i < 10; i++)
            {
                WebClient webClient = new WebClient();
                string result = webClient.DownloadString(String.Format("https://api.cognitive.microsoft.com/bing/v5.0/search?subscription-key={0}&q={1}&count=10&offset={2}&mkt=en-us&safesearch=Moderate&filter=webpages", apiKey, query, start));
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> collection = serializer.Deserialize<Dictionary<string, object>>(result);
                foreach (KeyValuePair<string, object> item in (IEnumerable)collection["webPages"])
                {
                    var y = item.Key;
                    if (y == "value")
                    {
                        var x = (ArrayList)item.Value;
                        foreach (Dictionary<string, object> o in x)
                        {
                            resultList.Results.Add(new Result
                            {
                                DisplayUrl = StringConvert(o["displayUrl"].ToString()),
                                Title = o["name"].ToString(),
                                Description = o["snippet"].ToString(),
                                Rank = count++

                            });
                        }
                    }
                }
                start += 10;
            }
            return resultList;
        }
    }
}
