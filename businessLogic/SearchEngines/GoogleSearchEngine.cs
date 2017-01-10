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
    public class GoogleSearchEngine : BaseSearchEngine, ISearchEngine, IAsyncSearchEngine
    {
        private const string ApiKey = "AIzaSyDAGFKL3kZevjzrFizgnVGnKmZNKUM1hjw";
        private const string Cx = "007172875963593911035:kpk5tcwf8pa";
        //private const string ApiKey = "AIzaSyB8kNz-iLRMinVRviNJHtJUkgPPOAx7mIk";
        //private const string Cx = "009511415247016879030:smaostb1cxe";

        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Google");
            var count = 1;
            uint start = 1;

            var webClient = new WebClient();
            try
            {
                for (var i = 0; i < 10; i++)
                {
                    var result =
                        webClient.DownloadString($"https://www.googleapis.com/customsearch/v1?key={ApiKey}&cx={Cx}&q={query}&start={start}&alt=json&cr=us");
                    var serializer = new JavaScriptSerializer();
                    var collection = serializer.Deserialize<Dictionary<string, object>>(result);
                    foreach (Dictionary<string, object> item in (IEnumerable) collection["items"])
                        if (!resultList.Results.Any(r => r.DisplayUrl.Equals(UrlConvert(item["link"].ToString()))))
                            resultList.Results.Add(new Result
                            {
                                DisplayUrl = UrlConvert(item["link"].ToString()),
                                Title = item["title"].ToString(),
                                Description = item["snippet"].ToString(),
                                Rank = count++
                            });
                    start += 10;
                }
            }
            catch (Exception)
            {
                return resultList;
            }
            return resultList;
        }

        public async Task<SearchEngineResultsList> AsyncSearch(string query)
        {
            var resultList = CreateSearchEngineResultsList("Google");
            resultList.Statistics.Name = "Google";
            resultList.Statistics.Start = DateTime.Now;
            var requests = new ConcurrentBag<Result>();

            await Task.Run(() =>
            {
                Parallel.For(0, NumberOfRequests, async i =>
                {
                    var request = await SingleSearchIteration(query, i);
                    foreach (var res in request)
                    {
                        requests.Add(res);
                    }
                });
            });

            resultList.Results = DistinctList(requests);
            resultList.Statistics.End = DateTime.Now;
            return resultList;
        }

        private async Task<List<Result>> SingleSearchIteration(string query, int i)
        {
            var count=1;
            var result = await SearchRequest(query, i);
            var serializer = new JavaScriptSerializer();
            var collection = serializer.Deserialize<Dictionary<string, object>>(result);
            return (
                from Dictionary<string, object> item in (IEnumerable) collection["items"]
                select new Result
                {
                    DisplayUrl = UrlConvert(item["link"].ToString()),
                    Title = item["title"].ToString(),
                    Description = item["snippet"].ToString(),
                    Rank = i*10 + count++
                }).ToList();
        }

        private Task<string> SearchRequest(string query, int page)
        {
            var webClient = new WebClient();
            var result =webClient.DownloadString($"https://www.googleapis.com/customsearch/v1?key={ApiKey}&cx={Cx}&q={query}&start={page*10+1}&alt=json&cr=us");
            webClient.Dispose();
            return Task.FromResult(result);
        }
    }
}