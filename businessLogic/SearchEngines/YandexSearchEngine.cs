using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace businessLogic.SearchEngines
{
    class YandexSearchEngine : BaseSearchEngine, ISearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = new SearchEngineResultsList
            {
                SearchEngineName = "Yandex",
                Results = new List<Result>()
            };
            string apiKey = "03.446094686:f1d118338db048a99bcc81892d8639c8";
            int count = 1;
            int page = 1;
            for (int i = 0; i < 10; i++)
            {
                page = i;
                WebRequest request = WebRequest.Create(String.Format("https://yandex.com/search/xml?l10n=en&user=itzikooper&key={0}&query={1}&page={2}", apiKey, query, page));
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                XElement xelement = XElement.Load(dataStream);
                IEnumerable<XElement> results = xelement.Elements();

                // var items = xelement.Element("results");
                foreach (var item in results?.Descendants("group"))
                {
                    resultList.Results.Add(new Result
                    {
                        Link = item.Element("doc").Element("url")?.Value,
                        Title = item.Element("doc").Element("title")?.Value,
                        Description = item.Element("doc").Element("headline")?.Value,
                        Rank = count++
                    });
                } 
            }

            return resultList;  
        }
    }
}
