using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using businessLogic.Interfaces;
using businessLogic.Models;
using System.Web.Script.Serialization;

namespace businessLogic.SearchEngines
{
    class BingSearchEngine : BaseSearchEngine, ISearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Bing");

            const string apiKey = "364dc7e685ed4672b4fa87ad9980d454";
            var count = 1;
            var start = 0;

            for (var i = 0; i < 1; i++)
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
                        var results = (ArrayList)item.Value;
                        foreach (Dictionary<string, object> res in results)
                        {
                            if (!resultList.Results.Any(r => r.DisplayUrl.Equals(UrlConvert(res["displayUrl"].ToString()))))
                            {
                                resultList.Results.Add(new Result
                                {
                                    DisplayUrl = UrlConvert(res["displayUrl"].ToString()),
                                    Title = res["name"].ToString(),
                                    Description = res["snippet"].ToString(),
                                    Rank = count++
                                }); 
                            }
                        }
                    }
                }
                start += 10;
            }
            return resultList;
        }
    }
}
