using System.Collections.Generic;
using businessLogic.Extentions;

namespace businessLogic.Models
{
    /// <summary>
    /// SearchEngineResultsList class collect all the results from single search engine.
    /// also provide the search engine name and statistics about the search.
    /// </summary>
    public class SearchEngineResultsList
    {
        public SearchEngineResultsList()
        {
            Statistics = new Statistics();
        }

        public string SearchEngineName { get; set; }
        public List<Result> Results { get; set; }
        public Statistics Statistics { get; set; }
    }
}