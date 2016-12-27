using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    public class BaseSearchEngine
    {
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

    }
}
