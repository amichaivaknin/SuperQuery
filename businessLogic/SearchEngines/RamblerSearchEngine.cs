using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;
using HtmlAgilityPack;

namespace businessLogic.SearchEngines
{
    public class RamblerSearchEngine : BaseSearchEngine, ISearchEngine, IAsyncSearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Rambler");
            var web = new HtmlWeb();

            for (var i = 1; i <= NumberOfRequests; i++)
            {
                var c = 1;
                var document =
                    web.Load(
                        $"http://nova.rambler.ru/search?scroll=1&utm_source=nhp&utm_content=search&utm_medium=button&utm_campaign=self_promo&query={query}&page={i}");
                var searchResults = document.DocumentNode.SelectNodes("//body").ToArray();
                var lWrapper = searchResults[0].SelectNodes("//div[@class='l-wrapper']//a").ToArray();
                var lContact = lWrapper[0].SelectNodes("//div[@class='l-content__results']//a").ToArray();
                var bSerpItems = lContact[0].SelectNodes("//div[@class='b-serp-item']//a").ToList();

                foreach (var bSerpItem in bSerpItems)
                {
                    var header = bSerpItem.SelectNodes("//h2[@class='b-serp-item__header']//a");
                    var snippet = bSerpItem.SelectNodes("//p[@class='b-serp-item__snippet']");

                    for (var j = 0; j < 10; j++)
                        resultList.Results.Add(new Result
                        {
                            DisplayUrl = UrlConvert(header[j].GetAttributeValue("href", null)),
                            Title = header[j].InnerText,
                            Description = snippet[j].InnerText,
                            Rank = c++
                        });
                    break;
                }
            }
            return resultList;
        }

        public async Task<SearchEngineResultsList> AsyncSearch(string query)
        {
            return await FullSearch(1, NumberOfRequests + 1, query, "Rambler");
            //var resultList = CreateSearchEngineResultsList("Rambler");
            //resultList.Statistics.Name = "Rambler";
            //resultList.Statistics.Start = DateTime.Now;
            //var requests = new ConcurrentBag<Result>();

            //await Task.Run(() =>
            //{
            //    Parallel.For(1, NumberOfRequests + 1, async i =>
            //    {
            //        try
            //        {
            //            var request = await SingleSearchIteration(query, i);
            //            foreach (var res in request)
            //            {
            //                requests.Add(res);
            //            }
            //        }
            //        catch
            //        {
            //            resultList.Statistics.Message = $"{resultList.Statistics.Message}/n request number {i} failed";
            //        }
            //    });
            //});

            //resultList.Results = OrderAndDistinctList(requests);
            //resultList.Statistics.End = DateTime.Now;
            //return resultList;
        }

        protected override async Task<List<Result>> SingleSearchIteration(string query, int page)
        {
            var resultList = new List<Result>();
            var c = page*10 - 10;
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

                for (var j = 0; j < 10 && header[j]!=null && snippet[j]!=null; j++)
                    resultList.Add(NewResult(header[j].GetAttributeValue("href", null), header[j].InnerText,
                        snippet[j].InnerText, c + j + 1));
                break;
            }
            return resultList;
        }

        private Task<HtmlDocument> SearchRequest(string query, int page)
        {
            try
            {
                var web = new HtmlWeb();
                var document =web.Load($"http://nova.rambler.ru/search?scroll=1&utm_source=nhp&utm_content=search&utm_medium=button&utm_campaign=self_promo&query={query}&page={page}");
                return Task.FromResult(document);
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}