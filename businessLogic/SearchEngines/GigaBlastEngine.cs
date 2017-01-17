using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    public class GigaBlastEngine : BaseSearchEngine, ISearchEngine, IAsyncSearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("GigaBlast");
            resultList.Statistics.Start = DateTime.Now;
            var count = 1;
            var webClient = new WebClient();
            var result =
                webClient.DownloadString(
                    $"http://www.gigablast.com/search?q={query}&format=json&n=100&rxivq=1015471771&rand=1482683517796");
            var serializer = new JavaScriptSerializer();
            var collection = serializer.Deserialize<Dictionary<string, object>>(result);

            foreach (Dictionary<string, object> res in (IEnumerable)collection["results"])
                if (!resultList.Results.Any(r => r.DisplayUrl.Equals(UrlConvert(res["url"].ToString()))))
                    resultList.Results.Add(new Result
                    {
                        DisplayUrl = UrlConvert(res["url"].ToString()),
                        Title = res["title"].ToString(),
                        Description = res["sum"].ToString(),
                        Rank = count++
                    });
            resultList.Statistics.End = DateTime.Now;
            return resultList;
        }

        public SearchEngineResultsList AsyncSearch(string query)
        {
            var resultList = CreateSearchEngineResultsList("GigaBlast");
            var count = 1;
            try
            {
                var result = SearchRequest(query);
                var serializer = new JavaScriptSerializer();
                var collection = serializer.Deserialize<Dictionary<string, object>>(result.Result);

                foreach (Dictionary<string, object> res in (IEnumerable)collection["results"])
                    resultList.Results.Add(NewResult(UrlConvert(res["url"].ToString()),
                        res["title"].ToString(), res["sum"].ToString(), count++));
                resultList.Results = OrderAndDistinctList(resultList.Results);

            }
            catch (System.Exception)
            {

                resultList.Statistics.Message = "access to GigaBlast failed";
            }
            return resultList;
        }
        private async Task<string> SearchRequest(string query)
        {
            using (var webClient = new WebClient())
            {
                var result =
                    await webClient.DownloadStringTaskAsync(
                        $"http://www.gigablast.com/search?q={query}&format=json&n={NumberOfRequests * 10}&rxivq=1015471771&rand=1482683517796");

                return result;
            }
        }
    }
}