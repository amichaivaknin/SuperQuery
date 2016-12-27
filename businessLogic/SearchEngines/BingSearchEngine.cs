using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;
using System.Web.Script.Serialization;

namespace businessLogic.SearchEngines
{
    public class BingSearchEngine : BaseSearchEngine, ISearchEngine, IAsyncSearchEngine
    {
        private const string ApiKey = "364dc7e685ed4672b4fa87ad9980d454";

        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Bing");
            var count = 1;
            var start = 0;

            for (var i = 0; i < 10; i++)
            {
                WebClient webClient = new WebClient();
                string result = webClient.DownloadString(String.Format("https://api.cognitive.microsoft.com/bing/v5.0/search?subscription-key={0}&q={1}&count=10&offset={2}&mkt=en-us&safesearch=Moderate&filter=webpages", ApiKey, query, start));
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

        public async Task<SearchEngineResultsList> AsyncSearch(string query)
        {
            var requests = new List<Task<List<Result>>>();

            for (var i = 0; i <= NumberOfRequests; i++)
            {
                requests.Add(SingleSearchIteration(query, i));
            }
            await Task.WhenAll(requests);

            var resultList = CreateSearchEngineResultsList("Bing");
            foreach (var request in requests.Where(request => request.Result != null))
            {
                resultList.Results.AddRange(request.Result);
            }

            resultList.Results = DistinctList(resultList.Results);
            return resultList;
        }

        private async Task<List<Result>> SingleSearchIteration(string query, int page)
        {
            var count = 1;
            var results = new List<Result>();
            var result = await SearchRequst(query, page);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> collection = serializer.Deserialize<Dictionary<string, object>>(result);

            foreach (var itemValue in from KeyValuePair<string, object> 
                         item in (IEnumerable)collection["webPages"]
                                      let y = item.Key where y == "value"
                                      select (ArrayList)item.Value)
                                      {
                                       results.AddRange(
                                          from Dictionary<string, object> res in itemValue
                                          select NewResult(UrlConvert(res["displayUrl"].ToString()), res["name"].ToString(),
                                          res["snippet"].ToString(), page*10 + count++));
                                       }
            return results;
        }

        private Task<string> SearchRequst(string query, int page)
        {
            WebClient webClient = new WebClient();
            string result = webClient.DownloadString($"https://api.cognitive.microsoft.com/bing/v5.0/search?subscription-key={ApiKey}&q={query}&count=10&offset={page*10}&mkt=en-us&safesearch=Moderate&filter=webpages");
            return Task.FromResult(result);
        }    
    }
}
