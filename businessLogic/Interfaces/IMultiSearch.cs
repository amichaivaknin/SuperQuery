using System.Collections.Generic;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
    internal interface IMultiSearch
    {
        IEnumerable<SearchEngineResultsList> GetResultsFromAllSearchEngines(string query);

        IEnumerable<SearchEngineResultsList> GetResultsFromSelectedSearchEngines(List<string> engines, string query);

        IEnumerable<SearchEngineResultsList> GetAsyncResultsFromAllSearchEngines(string query);

        IEnumerable<SearchEngineResultsList> GetAsyncResultsFromSelectedSearchEngines(List<string> engines, string query);
    }
}