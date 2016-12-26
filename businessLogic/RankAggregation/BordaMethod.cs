using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Models;
using System.Diagnostics;

namespace businessLogic.RankAggregation
{
    internal class BordaMethod
    {
        public List<FinalResult> BordaRank(IEnumerable<SearchEngineResultsList> allSearchResults)
        {
            var aggregationResults = new Dictionary<string, FinalResult>();
            //var descriptionDictionary = new Dictionary<string, FinalResult>();
            foreach (var searchResults in allSearchResults)
            {
                foreach (var result in searchResults.Results)
                {
                    var key = result.DisplayUrl;
                    var fl = false;

                    foreach (
                        var ar in
                        aggregationResults.Values.Where(
                            ar => CheckMatch(ar.DisplayUrl, result.DisplayUrl, ar.Description, result.Description)))
                    {
                        key = ar.DisplayUrl;
                        fl = true;
                        break;
                    }

                    if (!fl && !aggregationResults.ContainsKey(key))
                    {
                        aggregationResults.Add(key, new FinalResult
                        {
                            DisplayUrl = result.DisplayUrl,
                            Description = result.Description,
                            Title = result.Title
                        });
                    }
                    fl = false;
                    if (aggregationResults[key].Description == null)
                    {
                        aggregationResults[key].Description = result.Description;
                    }
                    aggregationResults[key].Rank += 100 - result.Rank;
                    aggregationResults[key].SearchEngines.Add(searchResults.SearchEngineName, (int) result.Rank);
                }
            }

            var orderedResults = from result in aggregationResults.Values
                orderby result.Rank descending
                select result;

            var x = orderedResults.ToList();
            return orderedResults.ToList();
        }

        private bool CheckMatch(string arUrl, string resultUrl, string description1, string description2)
        {
            return CheckUrl(arUrl, resultUrl) && CheckMatch(description1, description2);
        }

        private bool CheckUrl(string arUrl, string resultUrl)
        {
            try
            {
                Debug.WriteLine("first:    " + arUrl + "\nsecond:    " + resultUrl);
                if (arUrl.Equals(resultUrl)) return false;
                var ar = arUrl.IndexOf("/", StringComparison.Ordinal);
                var res = resultUrl.IndexOf("/", StringComparison.Ordinal);
                Debug.WriteLine("first:    " + arUrl.Substring(0, ar) + "\nsecond:    " + resultUrl.Substring(0, res));
                if (arUrl.Substring(0, ar).Equals(resultUrl.Substring(0, res)))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                
                return false;
            }
            return false;
        }

        private bool CheckMatch(string results, string resultDescription)
        {
            Debug.WriteLine("first:    " + results + "\nsecond:    " + resultDescription);
            if (resultDescription.Equals(""))
            {
                return false;
            }

            var length = 15;
                if (resultDescription.Length< length)
                {
                    length = resultDescription.Length;
                }
                
                return results.Contains(resultDescription.Substring(0, length));
        }

       
    }
}
