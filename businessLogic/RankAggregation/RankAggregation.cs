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
        private readonly object _lockMe = new object();

        public List<FinalResult> RankAndMerge(IEnumerable<SearchEngineResultsList> allSearchResults)
        {
            var aggregationResults = new ConcurrentDictionary<string, FinalResult>();

            Parallel.ForEach(allSearchResults, singleSearchEngineResults =>
            {
                foreach (var result in singleSearchEngineResults.Results)
                {
                    lock (_lockMe)
                    {
                        if (aggregationResults.ContainsKey(result.Link))
                        {

                            aggregationResults[result.Link].SearchEngines =
                                $"{aggregationResults[result.Link].SearchEngines} ,{singleSearchEngineResults.SearchEngineName}";
                            aggregationResults[result.Link].Rank += result.Rank;
                        }
                        else
                        {
                            FinalResult res;

                            res = new FinalResult
                            {
                                Link = result.Link,
                                Description = result.Description,
                                Rank = result.Rank,
                                SearchEngines = singleSearchEngineResults.SearchEngineName,
                                Title = result.Title
                            };

                            aggregationResults.TryAdd(result.Link, res);
                        }
                    }
                }
            });

            var resultsList = aggregationResults.Values.OrderBy(result => result.Rank).Reverse().ToList();
      
            return resultsList;
        }
    }
}
