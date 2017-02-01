using System.Collections.Generic;
using System.Linq;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic
{
    /// <summary>
    /// SuperQueryManager manage all the search process
    /// implament ISuperQueryManager interface
    /// </summary>
    public class SuperQueryManager : ISuperQueryManager
    {
        private readonly IMultiSearch _multiSearch;
        private readonly IRankAggregation _rankAggregation;

        public SuperQueryManager()
        {
            _multiSearch = new MultiSearch();
            _rankAggregation = new RankAggregation.RankAggregation();
        }

        /// <summary>
        /// Method for search at all search engines and return a aggregate results
        /// </summary>
        /// <param name="query">A string that insert by the user</param>
        /// <returns>return a aggregate results list</returns>
        public IEnumerable<FinalResult> GetQueryResults(string query)
        {
            var allSearchResults = _multiSearch.GetResultsFromAllSearchEngines(query);
            return _rankAggregation.BordaRank(allSearchResults);
        }

        /// <summary>
        /// Method for search at selected search engines and return a aggregate results
        /// </summary>
        /// <param name="engines">A list of search engines that user select for searching</param>
        /// <param name="query">A string that insert by the user</param>
        /// <returns>return a aggregate results list</returns>
        public IEnumerable<FinalResult> GetQueryResults(List<string> engines, string query)
        {
            var searchResults = _multiSearch.GetResultsFromSelectedSearchEngines(engines, query);
            var finalResults = _rankAggregation.BordaRank(searchResults);
            return finalResults.Take(100);
        }
    }
}