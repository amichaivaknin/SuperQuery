using System.Collections.Generic;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
    internal interface IMultiSearch
    {
        IEnumerable<SearchEngineResultsList> GetResultsFromAllSearchEngines(string query);

        IEnumerable<SearchEngineResultsList> GetResultsFromSelectedSearchEngines(List<string> engines, string query);
    }
}