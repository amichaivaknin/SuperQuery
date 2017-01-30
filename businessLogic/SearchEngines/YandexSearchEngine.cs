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
        private const string ApiKey = "03.446094686:f1d118338db048a99bcc81892d8639c8";

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
                    Rank = page * 10 + count++
                }).ToList();
        }

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

        //{
        //public SearchEngineResultsList Search(string query)
        //

        //  The original Search method before changes 
        //    var resultList = CreateSearchEngineResultsList("Yandex");
        //    resultList.Statistics.Start = DateTime.Now;
        //    var apiKey = "03.446094686:f1d118338db048a99bcc81892d8639c8";
        //    var count = 1;

        //    for (var i = 0; i < 10; i++)
        //    {
        //        var request = WebRequest.Create($"https://yandex.com/search/xml?l10n=en&user=itzikooper&key={apiKey}&query={query}&page={i}");
        //        var response = request.GetResponse();
        //        var dataStream = response.GetResponseStream();
        //        var xelement = XElement.Load(dataStream);
        //        var results = xelement.Elements();

        //        foreach (var item in results?.Descendants("group"))
        //        {
        //            var xElement = item.Element("doc");
        //            if (xElement == null) continue;

        //            var element = xElement.Element("headline");
        //            if (!resultList.Results.Any(r => r.DisplayUrl.Equals(UrlConvert(xElement.Element("url")?.Value))))
        //                resultList.Results.Add(new Result
        //                {
        //                    DisplayUrl = UrlConvert(xElement.Element("url")?.Value),
        //                    Title = xElement.Element("title")?.Value,
        //                    Description =
        //                        element != null
        //                            ? xElement.Element("headline").Value
        //                            : xElement.Element("passages").Element("passage").Value,
        //                    Rank = count++
        //                });
        //        }
        //    }
        //    resultList.Statistics.End = DateTime.Now;
        //    return resultList;
        //}

        // Not stable for this search engine
        //public SearchEngineResultsList ParallelSearch(string query)
        //{
        //    return FullSearch(0, NumberOfRequests, query, "Yandex");
        //}
    }
}