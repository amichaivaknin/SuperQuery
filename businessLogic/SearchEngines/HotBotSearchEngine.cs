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
    public class HotBotSearchEngine : BaseSearchEngine
    {
        public override SearchEngineResultsList Search(string query)
        {
            return FullSearch(1, NumberOfRequests + 1, query, "HotBot");
        }

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
                            Rank = (page - 1) * 10 + count
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

        private Task<HtmlDocument> SearchRequest(string query, int page)
        {
            var web = new HtmlWeb();
            var document = web.Load($"http://hotbot.com/search/?query={query}&page={page}");
            return Task.FromResult(document);
        }

        //  The original Search methods before changes 
        //
        //public SearchEngineResultsList Search(string query)
        //{
        //    var resultList = CreateSearchEngineResultsList("HotBot");
        //    resultList.Statistics.Start = DateTime.Now;
        //    var web = new HtmlWeb();

        //    for (var i = 1; i <= 10; i++)
        //    {
        //        var document = web.Load($"http://hotbot.com/search/?query={query}&page={i}");
        //        var searchResults = document.DocumentNode.SelectNodes("//div[@class='search-results']//a").ToArray();

        //        foreach (var node in searchResults)
        //        {
        //            var count = 1;
        //            foreach (var liTag in node.SelectNodes("//li"))
        //            {
        //                var titles = liTag.SelectNodes("//h3[@class='title']//a");
        //                var decriptions = liTag.SelectNodes("//div[@class='description']");
        //                foreach (var title in titles)
        //                {
        //                    resultList.Results.Add(new Result
        //                    {
        //                        DisplayUrl = UrlConvert(title.GetAttributeValue("href", null)),
        //                        Title = title.InnerText,
        //                        Description = decriptions[count].InnerText,
        //                        Rank = (i - 1) * 10 + count
        //                    });
        //                    count++;

        //                    if (count >= 10)
        //                        break;
        //                }
        //                if (count >= 10)
        //                    break;
        //            }
        //            if (count >= 10)
        //                break;
        //        }
        //    }
        //    resultList.Statistics.End = DateTime.Now;
        //    return resultList;
        //}

        //public SearchEngineResultsList ParallelSearch(string query)
        //{
        //    return FullSearch(1, NumberOfRequests + 1, query, "HotBot");
        //}


    }
}