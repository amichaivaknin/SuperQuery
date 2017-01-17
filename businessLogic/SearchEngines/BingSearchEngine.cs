using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    public class BingSearchEngine : BaseSearchEngine, ISearchEngine, IAsyncSearchEngine
    {
        private const string ApiKey = "364dc7e685ed4672b4fa87ad9980d454";

        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Bing");
            resultList.Statistics.Start = DateTime.Now;
            var count = 1;
            var start = 0;

            for (var i = 0; i < 10; i++)
            {
                var webClient = new WebClient();
                var result =
                    webClient.DownloadString(
                        $"https://api.cognitive.microsoft.com/bing/v5.0/search?subscription-key={ApiKey}&q={query}&count=10&offset={start}&mkt=en-us&safesearch=Moderate&filter=webpages");
                var serializer = new JavaScriptSerializer();
                var collection = serializer.Deserialize<Dictionary<string, object>>(result);

                foreach (KeyValuePair<string, object> item in (IEnumerable)collection["webPages"])
                {
                    var y = item.Key;
                    if (y == "value")
                    {
                        var results = (ArrayList)item.Value;
                        foreach (Dictionary<string, object> res in results)
                            if (
                                !resultList.Results.Any(
                                    r => r.DisplayUrl.Equals(UrlConvert(res["displayUrl"].ToString()))))
                                resultList.Results.Add(new Result
                                {
                                    DisplayUrl = UrlConvert(res["displayUrl"].ToString()),
                                    Title = res["name"].ToString(),
                                    Description = res["snippet"].ToString(),
                                    Rank = count++
                                });
                    }
                }
                start += 10;
            }

            resultList.Statistics.End= DateTime.Now;
            return resultList;
        }

        public SearchEngineResultsList AsyncSearch(string query)
        {
            return FullSearch(0, NumberOfRequests, query, "Bing");
        }

        //public async Task<SearchEngineResultsList> AsyncSearch(string query)
        //{
        //    return await FullSearch(0, NumberOfRequests, query, "Bing");
        //    //var resultList = CreateSearchEngineResultsList("Bing");
        //    //resultList.Statistics.Name = "Bing";
        //    //resultList.Statistics.Start = DateTime.Now;
        //    //var requests = new ConcurrentBag<Result>();

        //    //await Task.Run(() =>
        //    //{
        //    //    Parallel.For(0, NumberOfRequests, async i =>
        //    //    {
        //    //        var request = await SingleSearchIteration(query, i);
        //    //        foreach (var res in request)
        //    //        {
        //    //            requests.Add(res);
        //    //        }
        //    //    });
        //    //});

        //    //resultList.Results = OrderAndDistinctList(requests);
        //    //resultList.Statistics.End = DateTime.Now;
        //    //return resultList;
        //}

        protected override async Task<List<Result>> SingleSearchIteration(string query, int page)
        {
            var count = 1;
            var results = new List<Result>();
            var result = await SearchRequest(query, page);
            var serializer = new JavaScriptSerializer();
            var collection = serializer.Deserialize<Dictionary<string, object>>(result);

            foreach (var itemValue in from KeyValuePair<string, object>
                item in (IEnumerable) collection["webPages"]
                let y = item.Key
                where y == "value"
                select (ArrayList) item.Value)
                results.AddRange(
                    from Dictionary<string, object> res in itemValue
                    select NewResult(UrlConvert(res["displayUrl"].ToString()), res["name"].ToString(),
                        res["snippet"].ToString(), page*10 + count++));
            return results;
        }

        private async Task<string> SearchRequest(string query, int page)
        {
            using (var webClient = new WebClient())
            {
                var result = await webClient.DownloadStringTaskAsync($"https://api.cognitive.microsoft.com/bing/v5.0/search?subscription-key={ApiKey}&q={query}&count=10&offset={page * 10}&mkt=en-us&safesearch=Moderate&filter=webpages");

                return result;
            }
        }

    }
}