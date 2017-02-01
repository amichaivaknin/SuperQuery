using businessLogic.Models;

namespace businessLogic.Interfaces
{
/// <summary>
/// ISearchEngine interface, each search engine need to implement this interface
/// </summary>
    public interface ISearchEngine
    {
        /// <summary>
        /// this mathod get a query that insert by the user
        /// </summary>
        /// <param name="query">this query send to the search engine</param>
        /// <returns>return all the results that a specific search engine find</returns>
        SearchEngineResultsList Search(string query);
    }
}