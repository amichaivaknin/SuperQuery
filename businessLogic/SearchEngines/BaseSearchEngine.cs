using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    public abstract class BaseSearchEngine : ISearchEngine
    {
        protected const int NumberOfRequests = 10;

        public virtual SearchEngineResultsList Search(string query)
        {
            throw new NotImplementedException();
        }

        protected SearchEngineResultsList FullSearch(int startIndex, int endIndex, string query, string engineName)
        {
            var resultList = CreateSearchEngineResultsList(engineName);
            resultList.Statistics.Start = DateTime.Now;
            var requests = new ConcurrentBag<Result>();

            Parallel.For(startIndex, endIndex, i =>
            {
                try
                {
                    var request = SingleSearchIteration(query, i).Result;
                    foreach (var res in request)
                        requests.Add(res);
                }
                catch (Exception)
                {
                    resultList.Statistics.Message =
                        $"{resultList.Statistics.Message} requst no {i} failed {Environment.NewLine}";
                }
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