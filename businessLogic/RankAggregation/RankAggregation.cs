using System.Collections.Generic;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.RankAggregation
{
    internal class RankAggregation : IRankAggregation
    {
        public List<FinalResult> BordaRank(IEnumerable<SearchEngineResultsList> allSearchResults)
        {
            var borda = new BordaMethod();
            return borda.BordaRank(allSearchResults);
        }
    }
}