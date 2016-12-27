using System;
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
        private readonly Dictionary<string,ISearchEngine> _searchEngines;

        public MultiSearch()
        {
            _searchEngines = new Dictionary<string, ISearchEngine>
            {
                {"Google", new GoogleSearchEngine()},
                {"Bing", new BingSearchEngine()},
                {"Yandex", new YandexSearchEngine()},
                {"GigaBlast", new GigaBlastEngine()}
            };
        }

        public IEnumerable<SearchEngineResultsList> GetResultsFromAllSearchEngines(string query)
        {
            var allResults = new ConcurrentBag<SearchEngineResultsList>();

            Parallel.ForEach(_searchEngines.Values, searchEngine =>
            {
                allResults.Add(searchEngine.Search(query));
            });

            return allResults.ToList();
        }

        public IEnumerable<SearchEngineResultsList> GetResultsFromSelectedSearchEngines(List<string> engines, string query)
        {
            var allResults = new ConcurrentBag<SearchEngineResultsList>();

            Parallel.ForEach(engines, searchEngine =>
            {
                var engineResult = _searchEngines[searchEngine].Search(query);
                allResults.Add(engineResult);
            });
            return allResults.ToList();
        }
    }
}
