using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;
using businessLogic.SearchEngines;

namespace businessLogic
{
    internal class MultiSearch: IMultiSearch
    {
        private readonly List<ISearchEngine> _searchEngines;

        public MultiSearch()
        {
            _searchEngines = new List<ISearchEngine>
            {
                new AolSearchEngine(),
                new AskSearchEngine(),
                new BaiduSearchEngine(),
                new GoogleSearchEngine(),
                new YahooSearchEngine()
            };
        }

        public IEnumerable<SearchEngineResultsList> GetResultsFromAllSearchEngines(string query)
        {
            var allResults = new ConcurrentBag<SearchEngineResultsList>();

            Parallel.ForEach(_searchEngines, searchEngine =>
            {
                allResults.Add(searchEngine.Search(query));
            });

            return allResults.ToList();
        }
    }
}
