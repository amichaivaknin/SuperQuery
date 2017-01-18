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
    public class GoogleSearchEngine : BaseSearchEngine, ISearchEngine
    {
        private const string ApiKey = "AIzaSyDAGFKL3kZevjzrFizgnVGnKmZNKUM1hjw";
        private const string Cx = "007172875963593911035:kpk5tcwf8pa";
        //private const string ApiKey = "AIzaSyB8kNz-iLRMinVRviNJHtJUkgPPOAx7mIk";
        //private const string Cx = "009511415247016879030:smaostb1cxe";

        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Google");
            resultList.Statistics.Start = DateTime.Now;
            for (var i = 1; i <= NumberOfRequests; i++)
            {
                try
                {
                    var singleIterationResults = SingleSearchIteration(query, i).Result;
                    resultList.Results.AddRange(singleIterationResults);
                }
                catch (Exception)
                {
                    resultList.Statistics.Message = $"{resultList.Statistics.Message} requst no {i} failed {Environment.NewLine}";
                }
            }

            resultList.Results = OrderAndDistinctList(resultList.Results);
            resultList.Statistics.End = DateTime.Now;
            return resultList;
        }

        protected override async Task<List<Result>> SingleSearchIteration(string query, int i)
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
                    Rank = i*10 -10 + count++
                }).ToList();
        }

        private async Task<string> SearchRequest(string query, int page)
        {
            int start = (page * 10) - 9;
            using (var webClient = new WebClient())
            {
                var result = await webClient.DownloadStringTaskAsync($"https://www.googleapis.com/customsearch/v1?key={ApiKey}&cx={Cx}&q={query}&start={start}&alt=json&cr=us");

                return result;
            }
 
        }

        //  The original Search methods before changes 
        //
        //public SearchEngineResultsList Search(string query)
        //{
        //    var resultList = CreateSearchEngineResultsList("Google");
        //    resultList.Statistics.Start = DateTime.Now;
        //    var count = 1;
        //    uint start = 1;

        //    var webClient = new WebClient();
        //    try
        //    {
        //        for (var i = 0; i < 10; i++)
        //        {
        //            var result =
        //                webClient.DownloadString($"https://www.googleapis.com/customsearch/v1?key={ApiKey}&cx={Cx}&q={query}&start={start}&alt=json&cr=us");
        //            var serializer = new JavaScriptSerializer();
        //            var collection = serializer.Deserialize<Dictionary<string, object>>(result);
        //            foreach (Dictionary<string, object> item in (IEnumerable)collection["items"])
        //                if (!resultList.Results.Any(r => r.DisplayUrl.Equals(UrlConvert(item["link"].ToString()))))
        //                    resultList.Results.Add(new Result
        //                    {
        //                        DisplayUrl = UrlConvert(item["link"].ToString()),
        //                        Title = item["title"].ToString(),
        //                        Description = item["snippet"].ToString(),
        //                        Rank = count++
        //                    });
        //            start += 10;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return resultList;
        //    }

        //    resultList.Statistics.End = DateTime.Now;
        //    return resultList;
        //}

        //public SearchEngineResultsList ParallelSearch(string query)
        //{
        //    return FullSearch(1, NumberOfRequests + 1, query, "Google");
        //}
    }
}