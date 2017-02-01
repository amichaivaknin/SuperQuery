using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    /// <summary>
    /// GigaBlastEngine run a search on GigaBlast search engine
    /// No API
    /// </summary>
    public class GigaBlastEngine : BaseSearchEngine
    {
        /// <summary>
        /// Search mathod run a search according to NumberOfRequests
        /// GigaBlast allow as to get all the results in single request
        /// this mathod parsing a JSON file
        /// </summary>
        /// <param name="query">query that insert by user</param>
        /// <returns>GigaBlast search results</returns>
        public override SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("GigaBlast");
            resultList.Statistics.Start = DateTime.Now;
            var count = 1;
            try
            {
                var result = SearchRequest(query);
                var serializer = new JavaScriptSerializer();
                var collection = serializer.Deserialize<Dictionary<string, object>>(result.Result);

                foreach (Dictionary<string, object> res in (IEnumerable) collection["results"])
                    resultList.Results.Add(NewResult(UrlConvert(res["url"].ToString()),
                        res["title"].ToString(), res["sum"].ToString(), count++));
                resultList.Results = OrderAndDistinctList(resultList.Results);
            }
            catch (Exception)
            {
                resultList.Statistics.Message = "access to GigaBlast failed";
            }

            resultList.Results = OrderAndDistinctList(resultList.Results);
            resultList.Statistics.End = DateTime.Now;
            return resultList;
        }

        /// <summary>
        /// SearchRequest send the request to GigaBlast and waiting for results
        /// </summary>
        /// <param name="query"></param>
        /// <returns>string that contain all the data</returns>
        private Task<string> SearchRequest(string query)
        {
            string result;
            using (var webClient = new WebClient())
            {
                result =
                    webClient.DownloadString(
                        $"http://www.gigablast.com/search?q={query}&format=json&n={NumberOfRequests * 10}&rxivq=1015471771&rand=1482683517796");
            }
            return Task.FromResult(result);
        }
    }
}