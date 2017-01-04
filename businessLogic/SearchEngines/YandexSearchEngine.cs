using System.Linq;
using System.Net;
using System.Xml.Linq;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    public class YandexSearchEngine : BaseSearchEngine, ISearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Yandex");
            var apiKey = "03.446094686:f1d118338db048a99bcc81892d8639c8";
            var count = 1;

            for (var i = 0; i < 10; i++)
            {
                var request =
                    WebRequest.Create(
                        string.Format(
                            "https://yandex.com/search/xml?l10n=en&user=itzikooper&key={0}&query={1}&page={2}", apiKey,
                            query, i));
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
    }
}