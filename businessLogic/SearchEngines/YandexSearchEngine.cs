using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    public class YandexSearchEngine : BaseSearchEngine
    {
        /// <summary>
        /// YandexSearchEngine run a search on Yandex search engine
        /// use in Yandex API
        /// </summary>
        private const string ApiKey = "03.446094686:f1d118338db048a99bcc81892d8639c8";

        /// <summary>
        /// Search mathod run a search according to NumberOfRequests
        /// </summary>
        /// <param name="query">query that insert by user</param>
        /// <returns>Yandex search results</returns>
        public override SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Yandex");
            resultList.Statistics.Start = DateTime.Now;

            for (var i = 0; i < NumberOfRequests; i++)
                try
                {
                    var singleIterationResults = SingleSearchIteration(query, i).Result;
                    resultList.Results.AddRange(singleIterationResults);
                }
                catch (Exception)
                {
                    resultList.Statistics.Message =
                        $"{resultList.Statistics.Message} requst no {i} failed {Environment.NewLine}";
                }

            resultList.Results = OrderAndDistinctList(resultList.Results);
            resultList.Statistics.End = DateTime.Now;
            return resultList;
        }

        /// <summary>
        /// SingleSearchIteration parsing a XML file
        /// </summary>
        /// <param name="query"></param>
        /// <param name="i">Number of page that we want to get results for him</param>
        /// <returns>Search results of a single page</returns>
        protected override async Task<List<Result>> SingleSearchIteration(string query, int page)
        {
            var elements = await SearchRequest(query, page);
            var count = 0;
            return (from item in elements?.Descendants("group")
                select item.Element("doc")
                into xElement
                where xElement != null
                let element = xElement.Element("headline")
                select new Result
                {
                    DisplayUrl = UrlConvert(xElement.Element("url")?.Value),
                    Title = xElement.Element("title")?.Value,
                    Description =
                        element != null
                            ? xElement.Element("headline").Value
                            : xElement.Element("passages").Element("passage").Value,
                    Rank = page * 10 + count++ + 1
                }).ToList();
        }

        /// <summary>
        /// SearchRequest send the request to Yandex and waiting for results
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns>XElement that contain all the data</returns>
        private Task<IEnumerable<XElement>> SearchRequest(string query, int page)
        {
            var request =
                WebRequest.Create(
                    $"https://yandex.com/search/xml?l10n=en&user=itzikooper&key={ApiKey}&query={query}&page={page}");
            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            var xelement = XElement.Load(dataStream);
            var results = xelement.Elements();
            return Task.FromResult(results);
        }
    }
}