using System;
using System.Collections.Generic;
using System.Linq;
using businessLogic.Models;

namespace businessLogic.RankAggregation
{
    /// <summary>
    /// BordaMethod class rank and reorder the results according to borda rules
    /// </summary>
    internal class BordaMethod
    {  
        private const int StartRankPosition = 100;

        // _rankBySearchEngine is a dictionary that save the Weight according to search engine
        private readonly Dictionary<string, int> _rankBySearchEngine;

        public BordaMethod()
        {
            _rankBySearchEngine = new Dictionary<string, int>
            {
                {"Google", 100},
                {"Bing", 100},
                {"Yandex", 100},
                {"GigaBlast", 100},
                {"HotBot", 100},
                {"Rambler", 100}
            };
        }

        /// <summary>
        /// BordaRank do the process of ranking and ordering
        /// </summary>
        /// <param name="allSearchResults">List of all results from all/selected search engines</param>
        /// <returns>List of aggregate results</returns>
        public List<FinalResult> BordaRank(IEnumerable<SearchEngineResultsList> allSearchResults)
        {
            var searchEngineResultsLists = allSearchResults as IList<SearchEngineResultsList> ??
                                           allSearchResults.ToList();

            foreach (var searchEngine in searchEngineResultsLists)
                     SingleSearchEngineResultsRank(searchEngine.Results, searchEngine.SearchEngineName);

            var aggregationResults = BordaAggregate(searchEngineResultsLists);

            return (from result in aggregationResults.Values
                orderby result.Rank descending
                select result).ToList();
        }

        /// <summary>
        /// BordaAggregate marge between the results from all search engines according to borda rules
        /// </summary>
        /// <param name="searchEngineResultsLists">Results from all search engines</param>
        /// <returns>marge results, key = URL , value = FinalResult</returns>
        private Dictionary<string, FinalResult> BordaAggregate(IList<SearchEngineResultsList> searchEngineResultsLists)
        {
            var aggregationResults = new Dictionary<string, FinalResult>();
            foreach (var searchEngine in searchEngineResultsLists)
            foreach (var result in searchEngine.Results)
            {
                var location =
                    (int) ((result.Rank - StartRankPosition - _rankBySearchEngine[searchEngine.SearchEngineName]) * -1);
                if (aggregationResults.ContainsKey(result.DisplayUrl))
                {
                    aggregationResults[result.DisplayUrl].Rank += result.Rank;
                    if (aggregationResults[result.DisplayUrl].Description == "")
                        aggregationResults[result.DisplayUrl].Description = result.Description;
                    if (aggregationResults[result.DisplayUrl].Title == "")
                        aggregationResults[result.DisplayUrl].Title = result.Description;
                }
                else
                {
                    aggregationResults.Add(result.DisplayUrl, new FinalResult
                    {
                        Title = result.Title,
                        Description = result.Description,
                        DisplayUrl = result.DisplayUrl,
                        Rank = result.Rank,
                        SearchEngines = new Dictionary<string, int>()
                    });
                }
                aggregationResults[result.DisplayUrl].SearchEngines.Add(searchEngine.SearchEngineName, location);
            }
            return aggregationResults;
        }

        /// <summary>
        /// SingleSearchEngineResultsRank change the rank according to borda rules
        /// </summary>
        /// <param name="results">List of search results</param>
        /// <param name="searchEngineName">Search engine name</param>
        private void SingleSearchEngineResultsRank(List<Result> results, string searchEngineName)
        {
            foreach (var result in results)
                result.Rank = StartRankPosition - result.Rank + _rankBySearchEngine[searchEngineName];
        }
    }
}