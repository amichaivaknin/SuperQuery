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
                     if (!aggregationResults.ContainsKey(result.DisplayUrl))
                     {
                         aggregationResults.Add(result.DisplayUrl,new FinalResult
                         {
                             DisplayUrl = result.DisplayUrl,
                             Description = result.Description,
                             Title = result.Title
                         });
                     }

                     if (aggregationResults[result.DisplayUrl].Description == null)
                     {
                         aggregationResults[result.DisplayUrl].Description = result.Description;
                     }

                    aggregationResults[result.DisplayUrl].Rank += 100 - result.Rank;
                    aggregationResults[result.DisplayUrl].SearchEngines.Add(searchResults.SearchEngineName,(int)result.Rank);
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
