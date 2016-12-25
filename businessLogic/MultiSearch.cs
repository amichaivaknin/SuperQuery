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
       // private readonly List<ISearchEngine> _searchEngines;

        private readonly Dictionary<string,ISearchEngine> _searchEngines;

        public MultiSearch()
        {
            //_searchEngines = new List<ISearchEngine>
            //{
            //    new AolSearchEngine(),
            //    new AskSearchEngine(),
            //    new BaiduSearchEngine(),
            //    new GoogleSearchEngine(),
            //    new YahooSearchEngine()
            //};

            _searchEngines = new Dictionary<string, ISearchEngine>
            {
                {"Google", new GoogleSearchEngine()},
                {"Bing", new BingSearchEngine()},
                {"Yandex", new YandexSearchEngine()}
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
            throw new NotImplementedException();
        }
    }
}
