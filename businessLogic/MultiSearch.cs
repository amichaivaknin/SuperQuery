using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;
using businessLogic.SearchEngines;

namespace businessLogic
{
    /// <summary>
    /// MultiSearch search user query on all/selected search engines 
    /// implament IRankAggregation interface
    /// </summary>
    public class MultiSearch : IMultiSearch
    {
        private readonly Dictionary<string, ISearchEngine> _searchEngines;

        public MultiSearch()
        {
            _searchEngines = new Dictionary<string, ISearchEngine>
            {
                {"Google", new GoogleSearchEngine()},
                {"Bing", new BingSearchEngine()},
                {"Yandex", new YandexSearchEngine()},
                {"GigaBlast", new GigaBlastEngine()},
                {"HotBot", new HotBotSearchEngine()},
                {"Rambler", new RamblerSearchEngine()}
            };
        }

        /// <summary>
        /// Method for search at all search engines that define in this project in parallel
        /// </summary>
        /// <param name="query">A string that insert by the user</param>
        /// <returns>Return results from all search engines</returns>
        public IEnumerable<SearchEngineResultsList> GetResultsFromAllSearchEngines(string query)
        {
            var allResults = new ConcurrentBag<SearchEngineResultsList>();

            Parallel.ForEach(_searchEngines.Values, searchEngine => { allResults.Add(searchEngine.Search(query)); });

            return allResults;
        }

        /// <summary>
        /// Method for search at search engines that selected by the user in parallel
        /// </summary>
        /// <param name="engines">A list of search engines that user select for searching</param>
        /// <param name="query">A string that insert by the user</param>
        /// <returns>Return results from all search engines</returns>
        public IEnumerable<SearchEngineResultsList> GetResultsFromSelectedSearchEngines(List<string> engines,
            string query)
        {
            var allResults = new ConcurrentBag<SearchEngineResultsList>();

            Parallel.ForEach(engines, searchEngine =>
            {
                var engineResult = _searchEngines[searchEngine].Search(query);
                allResults.Add(engineResult);
            });
            return allResults;
        }
    }
}