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
    /// BingSearchEngine run a search on bing search engine
    /// use in bing API
    /// </summary>
    public class BingSearchEngine : BaseSearchEngine
    {
        private const string ApiKey = "364dc7e685ed4672b4fa87ad9980d454";

        /// <summary>
        /// Search mathod run a search according to NumberOfRequests
        /// </summary>
        /// <param name="query">query that insert by user</param>
        /// <returns>Bing search results</returns>
        public override SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Bing");
            resultList.Statistics.Start = DateTime.Now;

            for (var i = 0; i < NumberOfRequests; i++)
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
        /// <param name="page">Number of page that we want to get results for him</param>
        /// <returns>Search results of a single page</returns>
        protected override Task<List<Result>> SingleSearchIteration(string query, int page)
        {
            var count = 1;
            var results = new List<Result>();
            var result = SearchRequest(query, page);
            var serializer = new JavaScriptSerializer();
            var collection = serializer.Deserialize<Dictionary<string, object>>(result.Result);

            foreach (var itemValue in from KeyValuePair<string, object>
                item in (IEnumerable) collection["webPages"]
                let y = item.Key
                where y == "value"
                select (ArrayList) item.Value)
                results.AddRange(
                    from Dictionary<string, object> res in itemValue
                    select NewResult(UrlConvert(res["displayUrl"].ToString()), res["name"].ToString(),
                        res["snippet"].ToString(), page * 10 + count++));
            return Task.FromResult(results);
        }

        /// <summary>
        /// SearchRequest send the request to Bing and waiting for results
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns>string that contain all the data</returns>
        private Task<string> SearchRequest(string query, int page)
        {
            string result;
            using (var webClient = new WebClient())
            {
                result =
                    webClient.DownloadString(
                        $"https://api.cognitive.microsoft.com/bing/v5.0/search?subscription-key={ApiKey}&q={query}&count=10&offset={page * 10}&mkt=en-us&safesearch=Moderate&filter=webpages");
            }
            return Task.FromResult(result);
        }
    }
}