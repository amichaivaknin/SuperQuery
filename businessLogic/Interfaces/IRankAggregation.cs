using System.Collections.Generic;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
    /// <summary>
    /// IRankAggregation manage the rank aggregation process
    /// In our project we selected to use only in Borda mathod.
    /// </summary>
    internal interface IRankAggregation
    {
        /// <summary>
        ///BordaRank method get all the results from all/selected search engines,
        /// rank and reordering the results.
        /// </summary>
        /// <param name="allSearchResults">List of all results from all/selected search engines</param>
        /// <returns>List of aggregate results</returns>
        List<FinalResult> BordaRank(IEnumerable<SearchEngineResultsList> allSearchResults);
    }
}