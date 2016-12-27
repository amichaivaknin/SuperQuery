using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.RankAggregation
{
    internal class RankAggregation : IRankAggregation
    {
        //private readonly object _lockMe = new object();

        public List<FinalResult> BordaRank(IEnumerable<SearchEngineResultsList> allSearchResults)
        {
            var borda = new BordaMethod();
            return borda.BordaRank(allSearchResults);
        }

        //public List<FinalResult> RankAndMerge(IEnumerable<SearchEngineResultsList> allSearchResults)
        //{
        //    var aggregationResults = new ConcurrentDictionary<string, FinalResult>();

        //    Parallel.ForEach(allSearchResults, singleSearchEngineResults =>
        //    {
        //        foreach (var result in singleSearchEngineResults.Results)
        //        {
        //            lock (_lockMe)
        //            {
        //                if (aggregationResults.ContainsKey(result.DisplayUrl))
        //                {

        //                   // aggregationResults[result.DisplayUrl].SearchEngines =
        //                       // $"{aggregationResults[result.DisplayUrl].SearchEngines} ,{singleSearchEngineResults.SearchEngineName}";
        //                    aggregationResults[result.DisplayUrl].Rank += result.Rank;
        //                }
        //                else
        //                {
        //                    FinalResult res;

        //                    res = new FinalResult
        //                    {
        //                        DisplayUrl = result.DisplayUrl,
        //                        Description = result.Description,
        //                        Rank = result.Rank,
        //                        //SearchEngines = singleSearchEngineResults.SearchEngineName,
        //                        Title = result.Title
        //                    };

        //                    aggregationResults.TryAdd(result.DisplayUrl, res);
        //                }
        //            }
        //        }
        //    });

        //    var resultsList = aggregationResults.Values.OrderBy(result => result.Rank).Reverse().ToList();

        //    return resultsList;
        //}
    }
}
