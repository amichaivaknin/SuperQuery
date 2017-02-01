using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    /// <summary>
    /// GoogleSearchEngine run a search on Google search engine
    /// use in Google API
    /// </summary>
    public class GoogleSearchEngine : BaseSearchEngine
    {
        private const string ApiKey = "AIzaSyDAGFKL3kZevjzrFizgnVGnKmZNKUM1hjw";
        private const string Cx = "007172875963593911035:kpk5tcwf8pa";
        //private const string ApiKey = "AIzaSyB8kNz-iLRMinVRviNJHtJUkgPPOAx7mIk";
        //private const string Cx = "009511415247016879030:smaostb1cxe";

        /// <summary>
        /// Search mathod run a search according to NumberOfRequests
        /// </summary>
        /// <param name="query">query that insert by user</param>
        /// <returns>Google search results</returns>
        public override SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Google");
            resultList.Statistics.Start = DateTime.Now;
            for (var i = 1; i <= NumberOfRequests; i++)
                try
                {
                    var singleIterationResults = SingleSearchIteration(query, i).Result;
                    resultList.Results.AddRange(singleIterationResults);
                }
                catch (Exception)
                {
                    resultList.Statistics.Message =
                        $"{resultList.Statistics.Message} requst no {i} failed {Environment.NewLine}";
                }

            resultList.Results = OrderAndDistinctList(resultList.Results);
            resultList.Statistics.End = DateTime.Now;
            return resultList;
        }

        /// <summary>
        /// SingleSearchIteration parsing a JSON file
        /// </summary>
        /// <param name="query"></param>
        /// <param name="i">Number of page that we want to get results for him</param>
        /// <returns>Search results of a single page</returns>
        protected override async Task<List<Result>> SingleSearchIteration(string query, int i)
        {
            var count = 1;
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
                    Rank = i * 10 - 10 + count++
                }).ToList();
        }

        /// <summary>
        /// SearchRequest send the request to Google and waiting for results
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns>string that contain all the data</returns>
        private Task<string> SearchRequest(string query, int page)
        {
            var start = page * 10 - 9;
            using (var webClient = new WebClient())
            {
                var result =
                     webClient.DownloadString(
                        $"https://www.googleapis.com/customsearch/v1?key={ApiKey}&cx={Cx}&q={query}&start={start}&alt=json&cr=us");

                return Task.FromResult(result);
            }
        }
    }
}