using System.Collections.Generic;
using System.Linq;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic
{
    public class SuperQueryManager : ISuperQueryManager
    {
        private readonly IMultiSearch _multiSearch;
        private readonly IRankAggregation _rankAggregation;

        public SuperQueryManager()
        {
            _multiSearch = new MultiSearch();
            _rankAggregation = new RankAggregation.RankAggregation();
        }

        public IEnumerable<FinalResult> GetQueryResults(string query)
        {
            var allSearchResults = _multiSearch.GetAsyncResultsFromAllSearchEngines(query);
            return _rankAggregation.BordaRank(allSearchResults);
        }

        public IEnumerable<FinalResult> GetQueryResults(List<string> engines, string query)
        {
            var searchResults = _multiSearch.GetAsyncResultsFromSelectedSearchEngines(engines, query);
            var finalResults = _rankAggregation.BordaRank(searchResults);
            return finalResults.Take(100);
        }
    }
}