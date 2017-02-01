using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    /// <summary>
    /// BaseSearchEngine is a abstract class that all search engines need to ininheritance 
    /// implament ISearchEngine interface
    /// </summary>
    public abstract class BaseSearchEngine : ISearchEngine
    {
        //number of pages from search engine
        protected const int NumberOfRequests = 10;

        //implement in the derive classes
        public virtual SearchEngineResultsList Search(string query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ParallelSearch send all the requests in parallel in order to save time
        /// not all the search engines alow as to send parallel requests 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="query"></param>
        /// <param name="engineName"></param>
        /// <returns></returns>
        /// 
        protected SearchEngineResultsList ParallelSearch(int startIndex, int endIndex, string query, string engineName)
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

        //implement in the derive classes
        protected virtual Task<List<Result>> SingleSearchIteration(string query, int page)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// UrlConvert set the url to appropriate url
        /// </summary>
        /// <param name="value">URL of result</param>
        /// <returns>URL of result</returns>
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

        /// <summary>
        /// OrderAndDistinctList search for duplicate results and remove them
        /// order the list by result rank.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
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