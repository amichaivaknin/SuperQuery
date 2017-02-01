using System.Collections.Generic;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
    /// <summary>
    /// ISuperQueryManager manage all the search process
    /// Include the search on all/selected search engines and rank aggregation
    /// </summary>
    public interface ISuperQueryManager
    {
        IEnumerable<FinalResult> GetQueryResults(string query);

        IEnumerable<FinalResult> GetQueryResults(List<string> engines, string query);
    }
}