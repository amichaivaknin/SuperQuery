using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Models;

namespace businessLogic.RankAggregation
{
    internal class BordaMethod
    {
         public List<FinalResult> BordaRank(IEnumerable<SearchEngineResultsList> allSearchResults)
         {
            var aggregationResults = new Dictionary<string, FinalResult>();
             foreach (var searchResults in allSearchResults)
             {
                foreach (var result in searchResults.Results)
                 {
                     if (!aggregationResults.ContainsKey(result.Link))
                     {
                         aggregationResults.Add(result.Link,new FinalResult
                         {
                             Link = result.Link,
                             Description = result.Description,
                             Title = result.Title
                         });
                     }

                     if (aggregationResults[result.Link].Description == null)
                     {
                         aggregationResults[result.Link].Description = result.Description;
                     }

                    aggregationResults[result.Link].Rank += 100 - result.Rank;
                    aggregationResults[result.Link].SearchEngines.Add(searchResults.SearchEngineName,(int)result.Rank);
                 }
             }

             var orderedResults = from result in aggregationResults.Values
                                  orderby result.Rank descending
                                  select result;

             var x = orderedResults.ToList();
             return orderedResults.ToList();
         }
    }
}
