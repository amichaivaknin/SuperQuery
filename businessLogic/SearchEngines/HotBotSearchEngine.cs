using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using businessLogic.Models;
using HtmlAgilityPack;

namespace businessLogic.SearchEngines
{
    /// <summary>
    /// HotBotSearchEngine run a search on HotBot search engine
    /// No API
    /// </summary>
    public class HotBotSearchEngine : BaseSearchEngine
    {
        /// <summary>
        /// Search mathod run a search according to NumberOfRequests
        /// use in ParallelSearch
        /// </summary>
        /// <param name="query">query that insert by user</param>
        /// <returns>HotBot search results</returns>
        public override SearchEngineResultsList Search(string query)
        {
            return ParallelSearch(1, NumberOfRequests + 1, query, "HotBot");
        }

        /// <summary>
        /// SingleSearchIteration parsing a HTML file
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page">Number of page that we want to get results for him</param>
        /// <returns>Search results of a single page</returns>
        protected override async Task<List<Result>> SingleSearchIteration(string query, int page)
        {
            var resultList = new List<Result>();
            var document = await SearchRequest(query, page);
            var searchResults = document.DocumentNode.SelectNodes("//div[@class='search-results']//a").ToArray();

            foreach (var node in searchResults)
            {
                var count = 0;
                foreach (var liTag in node.SelectNodes("//li"))
                {
                    var titles = liTag.SelectNodes("//h3[@class='title']//a");
                    var decriptions = liTag.SelectNodes("//div[@class='description']");
                    foreach (var title in titles)
                    {
                        resultList.Add(new Result
                        {
                            DisplayUrl = UrlConvert(title.GetAttributeValue("href", null)),
                            Title = title.InnerText,
                            Description = decriptions[count].InnerText,
                            Rank = (page - 1) * 10 + count+1
                        });
                        count++;
                    }
                    if (count >= 10)
                        break;
                }
                if (count >= 10)
                    break;
            }

            return resultList;
        }

        /// <summary>
        /// SearchRequest send the request to HotBot and waiting for results
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns>HtmlDocument that contain all the data</returns>
        private Task<HtmlDocument> SearchRequest(string query, int page)
        {
            var web = new HtmlWeb();
            var document = web.Load($"http://hotbot.com/search/?query={query}&page={page}");
            return Task.FromResult(document);
        }
    }
}