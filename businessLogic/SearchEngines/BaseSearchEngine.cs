using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using businessLogic.Extentions;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    public class BaseSearchEngine
    {
        protected const int NumberOfRequests = 1;

        protected async Task<SearchEngineResultsList> FullSearch(int startIndex, int endIndex, string query, string engineName)
        {
            var resultList = CreateSearchEngineResultsList(engineName);
            resultList.Statistics.Name = engineName;
            resultList.Statistics.Start = DateTime.Now;
            var requests = new ConcurrentBag<Result>();

            await Task.Run(() =>
            {
                Parallel.For(startIndex, endIndex, async i =>
                {

                    try
                    {
                        var request = await SingleSearchIteration(query, i);
                        foreach (var res in request)
                        {
                            requests.Add(res);
                        }
                    }
                    catch (Exception)
                    {

                        resultList.Statistics.Message = $"{resultList.Statistics.Message} requst no {i} failed {Environment.NewLine}";
                    }
                });
            });

            resultList.Results = OrderAndDistinctList(requests);
            resultList.Statistics.End = DateTime.Now;
            return resultList;
        }

        protected virtual Task<List<Result>> SingleSearchIteration(string query, int page)
        {
            return null;
        }

        protected string UrlConvert(string value)
        {
            return value.Replace("https://", "").Replace("http://", "").Replace("www.", "").TrimEnd('/').TrimEnd('\\');
        }

        protected SearchEngineResultsList CreateSearchEngineResultsList(string searchEngineName)
        {
            return new SearchEngineResultsList
            {
                SearchEngineName = searchEngineName,

                Results = new List<Result>()
            };
        }
       
        protected List<Result> OrderAndDistinctList(IEnumerable<Result> results)
        {
            return results.OrderBy(x => x.Rank).GroupBy(x => x.DisplayUrl).Select(y => y.First()).ToList();
        }

        protected Result NewResult(string displayUrl, string title, string description, int rank)
        {
            return new Result
            {
                DisplayUrl = displayUrl,
                Title = title,
                Description = description,
                Rank = rank
            };
        }
    }
}