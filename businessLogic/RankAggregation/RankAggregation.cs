using System.Collections.Generic;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.RankAggregation
{
    /// <summary>
    /// RankAggregation class mamage all the rank aggregation process
    /// implament IRankAggregation interface
    /// </summary>
    internal class RankAggregation : IRankAggregation
    {
        /// <summary>
        ///BordaRank method get all the results from all/selected search engines,
        /// rank and reordering the results.
        /// </summary>
        /// <param name="allSearchResults">List of all results from all/selected search engines</param>
        /// <returns>List of aggregate results</returns>
        public List<FinalResult> BordaRank(IEnumerable<SearchEngineResultsList> allSearchResults)
        {
            var borda = new BordaMethod();
            return borda.BordaRank(allSearchResults);
        }
    }
}