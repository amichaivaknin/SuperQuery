using System;
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
        public async Task<SearchEngineResultsList> AsyncSearch(string query)
        {
            var resultList = CreateSearchEngineResultsList("Rambler");
            resultList.Statistics.Name = "Rambler";
            resultList.Statistics.Start= DateTime.Now;
            var requests = new List<Task<List<Result>>>();

            for (var i = 1; i <= NumberOfRequests; i++)
                requests.Add(SingleSearchIteration(query, i));
            await Task.WhenAll(requests);

            

            foreach (var request in requests.Where(request => request.Result != null))
                resultList.Results.AddRange(request.Result);

            resultList.Results = DistinctList(resultList.Results);
            resultList.Statistics.End=DateTime.Now;
            return resultList;
        }

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

        private async Task<List<Result>> SingleSearchIteration(string query, int page)
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

                for (var j = 0; j < 10; j++)
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
                var document =
                    web.Load(
                        $"http://nova.rambler.ru/search?scroll=1&utm_source=nhp&utm_content=search&utm_medium=button&utm_campaign=self_promo&query={query}&page={page}");
                return Task.FromResult(document);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}