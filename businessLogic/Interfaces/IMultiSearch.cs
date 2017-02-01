using System.Collections.Generic;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
    /// <summary>
    /// IMultiSearch for search user query on all/selected search engines 
    /// </summary>
    internal interface IMultiSearch
    {
        /// <summary>
        /// Method declaration for search at all search engines that define in this project
        /// </summary>
        /// <param name="query">A string that insert by the user</param>
        /// <returns>Return results from all search engines</returns>
        IEnumerable<SearchEngineResultsList> GetResultsFromAllSearchEngines(string query);

        /// <summary>
        /// Method declaration for search at search engines that selected by the user
        /// </summary>
        /// <param name="engines">A list of search engines that user select for searching</param>
        /// <param name="query">A string that insert by the user</param>
        /// <returns>Return results from all search engines</returns>
        IEnumerable<SearchEngineResultsList> GetResultsFromSelectedSearchEngines(List<string> engines, string query);
    }
}