using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using businessLogic.Models;
using HtmlAgilityPack;

namespace businessLogic.SearchEngines
{
    /// <summary>
    /// RamblerSearchEngine run a search on Rambler search engine
    /// No API
    /// </summary>
    public class RamblerSearchEngine : BaseSearchEngine
    {
        /// <summary>
        /// Search mathod run a search according to NumberOfRequests
        /// use in ParallelSearch
        /// </summary>
        /// <param name="query">query that insert by user</param>
        /// <returns>Rambler search results</returns>
        public override SearchEngineResultsList Search(string query)
        {
            return ParallelSearch(1, NumberOfRequests + 1, query, "Rambler");
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
            var c = page * 10 - 10;
            var document = await SearchRequest(query, page);

            if (document == null)
                return null;

            var searchResults = document.DocumentNode.SelectNodes("//body").ToArray();
            var lWrapper = searchResults[0].SelectNodes("//div[@class='l-wrapper']//a").ToArray();
            var lContact = lWrapper[0].SelectNodes("//div[@class='l-content__results']//a").ToArray();
            var bSerpItems = lContact[0].SelectNodes("//div[@class='b-serp-item']//a").ToList();

            foreach (var bSerpItem in bSerpItems)
            {
                var header = bSerpItem.SelectNodes("//h2[@class='b-serp-item__header']//a");
                var snippet = bSerpItem.SelectNodes("//p[@class='b-serp-item__snippet']");

                for (var j = 0; j < 10 && header[j] != null && snippet[j] != null; j++)
                    resultList.Add(NewResult(UrlConvert(header[j].GetAttributeValue("href", null)), header[j].InnerText,
                        snippet[j].InnerText, c + j + 1));
                break;
            }
            return resultList;
        }

        /// <summary>
        /// SearchRequest send the request to Rambler and waiting for results
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns>HtmlDocument that contain all the data</returns>
        private Task<HtmlDocument> SearchRequest(string query, int page)
        {
            var web = new HtmlWeb();
            var document =
                web.Load(
                    $"http://nova.rambler.ru/search?scroll=1&utm_source=nhp&utm_content=search&utm_medium=button&utm_campaign=self_promo&query={query}&page={page}");
            return Task.FromResult(document);
        }
    }
}