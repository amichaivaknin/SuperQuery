using System;
using System.Collections.Generic;
using System.Linq;
using businessLogic.Models;

namespace businessLogic.RankAggregation
{
    internal class BordaMethod
    {
        private const int StartRankPosition = 100;
        private readonly Dictionary<string, int> _rankBySearchEngine;

        public BordaMethod()
        {
            _rankBySearchEngine = new Dictionary<string, int>
            {
                {"Google", 600},
                {"Bing", 600},
                {"Yandex", 600},
                {"GigaBlast", 600},
                {"HotBot", 600},
                {"Rambler", 600}
            };
        }

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
            //RankAndMargeResultsFromAllLists(searchEngineResultsLists, aggregationResults);
        }

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

        private void SingleSearchEngineResultsRank(List<Result> results, string searchEngineName)
        {
            foreach (var result in results)
                result.Rank = StartRankPosition - result.Rank + _rankBySearchEngine[searchEngineName];
        }

        //    const int startRankPosition = 100;
        //{
        //    SearchEngineResultsList searchResults)

        //private void RankSingleResultsList(Dictionary<string, FinalResult> aggregationResults,
        //}
        //        RankSingleResultsList(aggregationResults, searchResults);
        //    foreach (var searchResults in allSearchResults)
        //{
        //    Dictionary<string, FinalResult> aggregationResults)

        //private void RankAndMargeResultsFromAllLists(IEnumerable<SearchEngineResultsList> allSearchResults,

        //    foreach (var result in searchResults.Results)
        //    {
        //        var key = result.DisplayUrl;

        //        if (!aggregationResults.ContainsKey(result.DisplayUrl))
        //        {
        //            var fl = false;
        //            foreach (var ar in aggregationResults.Values.
        //                Where(ar => CheckMatch(ar.DisplayUrl, result.DisplayUrl, ar.Description, result.Description)))
        //            {
        //                key = ar.DisplayUrl;
        //                fl = true;
        //                break;
        //            }

        //            if (!fl)
        //                AddNewFinalResult(aggregationResults, key, result);
        //        }

        //        if (aggregationResults[key].Description == null)
        //            aggregationResults[key].Description = result.Description;

        //        if (!aggregationResults[key].SearchEngines.ContainsKey(searchResults.SearchEngineName))
        //        {
        //            aggregationResults[key].Rank += startRankPosition - result.Rank +
        //                                            _rankBySearchEngine[searchResults.SearchEngineName];
        //            aggregationResults[key].SearchEngines.Add(searchResults.SearchEngineName, (int)result.Rank);
        //        }
        //    }
        //}

        //private static void AddNewFinalResult(IDictionary<string, FinalResult> aggregationResults, string key,
        //    Result result)
        //{
        //    aggregationResults.Add(key, new FinalResult
        //    {
        //        DisplayUrl = result.DisplayUrl,
        //        Description = result.Description,
        //        Title = result.Title
        //    });
        //}
    }
}