using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;
using businessLogic.SearchEngines;


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
            var allSearchResults = _multiSearch.GetResultsFromAllSearchEngines(query);
            return  _rankAggregation.RankAndMerge(allSearchResults);
        }

        public IEnumerable<FinalResult> GetQueryResults(List<string> engines, string query)
        {
            var searchResults = _multiSearch.GetResultsFromSelectedSearchEngines(engines,query);
            var x =  _rankAggregation.BordaRank(searchResults);
            return x;
        }
    }
}
