using System.Linq;
using businessLogic.Interfaces;
using businessLogic.Models;
using HtmlAgilityPack;

namespace businessLogic.SearchEngines
{
    internal class HotBotSearchEngine : BaseSearchEngine, ISearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("HotBot");
            var web = new HtmlWeb();

            for (var i = 1; i <= 10; i++)
            {
                var document = web.Load($"http://hotbot.com/search/?query={query}&page={i}");
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
                            resultList.Results.Add(new Result
                            {
                                DisplayUrl = UrlConvert(title.GetAttributeValue("href", null)),
                                Title = title.InnerText,
                                Description = decriptions[count].InnerText
                            });
                            count++;

                            if (count >= 10)
                                break;
                        }
                        if (count >= 10)
                            break;
                    }
                    if (count >= 10)
                        break;
                }
            }

            return resultList;
        }
    }
}