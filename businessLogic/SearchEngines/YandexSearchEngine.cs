using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    public class YandexSearchEngine : BaseSearchEngine, ISearchEngine, IAsyncSearchEngine
    {
        private const string ApiKey = "03.446094686:f1d118338db048a99bcc81892d8639c8";

        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Yandex");
            var apiKey = "03.446094686:f1d118338db048a99bcc81892d8639c8";
            var count = 1;

            for (var i = 0; i < 10; i++)
            {
                var request =WebRequest.Create( $"https://yandex.com/search/xml?l10n=en&user=itzikooper&key={apiKey}&query={query}&page={i}");
                var response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                var xelement = XElement.Load(dataStream);
                var results = xelement.Elements();

                foreach (var item in results?.Descendants("group"))
                {
                    var xElement = item.Element("doc");
                    if (xElement == null) continue;

                    var element = xElement.Element("headline");
                    if (!resultList.Results.Any(r => r.DisplayUrl.Equals(UrlConvert(xElement.Element("url")?.Value))))
                        resultList.Results.Add(new Result
                        {
                            DisplayUrl = UrlConvert(xElement.Element("url")?.Value),
                            Title = xElement.Element("title")?.Value,
                            Description =
                                element != null
                                    ? xElement.Element("headline").Value
                                    : xElement.Element("passages").Element("passage").Value,
                            Rank = count++
                        });
                }
            }

            return resultList;
        }

        public async Task<SearchEngineResultsList> AsyncSearch(string query)
        {
            return await FullSearch(0, NumberOfRequests, query, "Yandex");
        }

        protected override Task<List<Result>> SingleSearchIteration(string query, int page)
        {
            return base.SingleSearchIteration(query, page);
        }

        private Task<IEnumerable<XElement>> SearchRequest(string query, int page)
        {
            var request = WebRequest.Create($"https://yandex.com/search/xml?l10n=en&user=itzikooper&key={ApiKey}&query={query}&page={page}");
            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            var xelement = XElement.Load(dataStream);
            var results = xelement.Elements();
            return Task.FromResult(results);
        }
    }
}