using System;
using System.Collections.Generic;
using System.Linq;
using businessLogic.Models;

namespace businessLogic.RankAggregation
{
    internal class BordaMethod
    {
        private readonly Dictionary<string, int> _rankBySearchEngine;
        private const int StartRankPosition = 100;
        public BordaMethod()
        {
            _rankBySearchEngine = new Dictionary<string, int>
            {
                {"Google", 10},
                {"Bing", 10},
                {"Yandex", 10},
                {"GigaBlast", 10},
                {"HotBot", 10},
                {"Rambler", 10}
            };
        }

        public List<FinalResult> BordaRank(IEnumerable<SearchEngineResultsList> allSearchResults)
        {
            var aggregationResults = new Dictionary<string, FinalResult>();

            //foreach (var searchEngine in allSearchResults)
            //{
            //    var singleSEarchEngineRankDictionary = SingleSearchEngineResultsRank(searchEngine.Results,searchEngine.SearchEngineName);

            //    foreach (var result in singleSEarchEngineRankDictionary)
            //    {
            //        if (!aggregationResults.ContainsKey(result.Key))
            //        {
            //            var location = (int)((result.Value.Rank - StartRankPosition - _rankBySearchEngine[searchEngine.SearchEngineName])*-1);
            //            aggregationResults.Add(result.Key, new FinalResult
            //            {
            //                Description = result.Value.Description,
            //                DisplayUrl = result.Value.DisplayUrl,
            //                Rank = result.Value.Rank,
            //                SearchEngines = new Dictionary<string, int> {{searchEngine.SearchEngineName, location}}
            //            });
            //        }
            //        else
            //        {
                        
            //        }
            //    }
            //}

            RankAndMargeResultsFromAllLists(allSearchResults, aggregationResults);


            var x = (from result in aggregationResults.Values
                     orderby result.Rank descending
                     select result).ToList();

            return x;
        }

        private Dictionary<string, Result> SingleSearchEngineResultsRank(List<Result> results, string searchEngineName)
        {
            var resultsMap = new Dictionary<string, Result>();
            foreach (var result in results)
            {
                result.Rank = StartRankPosition - result.Rank +_rankBySearchEngine[searchEngineName];
                resultsMap.Add(result.DisplayUrl, result);
            }
            return resultsMap;
        }

        private void RankAndMargeResultsFromAllLists(IEnumerable<SearchEngineResultsList> allSearchResults,
            Dictionary<string, FinalResult> aggregationResults)
        {
            foreach (var searchResults in allSearchResults)
                RankSingleResultsList(aggregationResults, searchResults);
        }

        private void RankSingleResultsList(Dictionary<string, FinalResult> aggregationResults,
            SearchEngineResultsList searchResults)
        {
            const int startRankPosition = 100;

            foreach (var result in searchResults.Results)
            {
                var key = result.DisplayUrl;

                if (!aggregationResults.ContainsKey(result.DisplayUrl))
                {
                    var fl = false;
                    foreach (var ar in aggregationResults.Values.
                        Where(ar => CheckMatch(ar.DisplayUrl, result.DisplayUrl, ar.Description, result.Description)))
                    {
                        key = ar.DisplayUrl;
                        fl = true;
                        break;
                    }

                    if (!fl)
                        AddNewFinalResult(aggregationResults, key, result);
                }

                if (aggregationResults[key].Description == null)
                    aggregationResults[key].Description = result.Description;

                if (!aggregationResults[key].SearchEngines.ContainsKey(searchResults.SearchEngineName))
                {
                    aggregationResults[key].Rank += startRankPosition - result.Rank +
                                                    _rankBySearchEngine[searchResults.SearchEngineName];
                    aggregationResults[key].SearchEngines.Add(searchResults.SearchEngineName, (int) result.Rank);
                }
            }
        }

        private static void AddNewFinalResult(IDictionary<string, FinalResult> aggregationResults, string key,
            Result result)
        {
            aggregationResults.Add(key, new FinalResult
            {
                DisplayUrl = result.DisplayUrl,
                Description = result.Description,
                Title = result.Title
            });
        }

        private static bool CheckMatch(string arUrl, string resultUrl, string description1, string description2)
        {
            return CheckUrl(arUrl, resultUrl) && CheckDescriptionMatch(description1, description2);
        }

        private static bool CheckUrl(string arUrl, string resultUrl)
        {
            if (arUrl.Contains("/") && resultUrl.Contains("/"))
            {
                if (arUrl.Equals(resultUrl)) return false;
                var ar = arUrl.IndexOf("/", StringComparison.Ordinal);
                var res = resultUrl.IndexOf("/", StringComparison.Ordinal);
                //Debug.WriteLine("first:    " + arUrl.Substring(0, ar) + "\nsecond:    " + resultUrl.Substring(0, res));
                return arUrl.Substring(0, ar).Equals(resultUrl.Substring(0, res));
            }
            return false;
        }

        private static bool CheckDescriptionMatch(string results, string resultDescription)
        {
            //Debug.WriteLine("first:    " + results + "\nsecond:    " + resultDescription);
            if (!resultDescription.Equals(""))
            {
                var length = 15;
                if (resultDescription.Length < length)
                    length = resultDescription.Length;
                return results.Contains(resultDescription.Substring(0, length));
            }
            return false;
        }
    }
}