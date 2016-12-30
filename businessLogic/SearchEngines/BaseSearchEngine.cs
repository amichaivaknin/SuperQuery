using System.Collections.Generic;
using System.Linq;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    public class BaseSearchEngine
    {
        protected const int NumberOfRequests = 10;

        internal string UrlConvert(string value)
        {
            return value.Replace("https://", "").Replace("http://", "").Replace("www.", "").TrimEnd('/').TrimEnd('\\');
        }

        internal SearchEngineResultsList CreateSearchEngineResultsList(string searchEngineName)
        {
            return new SearchEngineResultsList
            {
                SearchEngineName = searchEngineName,
                Results = new List<Result>()
            };
        }

        internal List<Result> DistinctList(IEnumerable<Result> results)
        {
            return results.GroupBy(x => x.DisplayUrl).Select(y => y.First()).ToList();
        }

        protected Result NewResult(string displayUrl, string title, string description, int rank)
        {
            return new Result
            {
                DisplayUrl = displayUrl,
                Title = title,
                Description = description,
                Rank = rank
            };
        }
    }
}