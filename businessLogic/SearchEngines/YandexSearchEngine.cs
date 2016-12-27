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
    internal class YandexSearchEngine : BaseSearchEngine, ISearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("Yandex");
            string apiKey = "03.446094686:f1d118338db048a99bcc81892d8639c8";
            int count = 1;
     
            for (int i = 0; i < 10; i++)
            {   
                WebRequest request = WebRequest.Create(String.Format("https://yandex.com/search/xml?l10n=en&user=itzikooper&key={0}&query={1}&page={2}", apiKey, query, i));
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                XElement xelement = XElement.Load(dataStream);
                IEnumerable<XElement> results = xelement.Elements();

                foreach (var item in results?.Descendants("group"))
                {
                    var xElement = item.Element("doc");
                    if (xElement == null) continue;

                    var element = xElement.Element("headline");
                    if (!resultList.Results.Any(r => r.DisplayUrl.Equals(StringConvert(xElement.Element("url")?.Value))))
                    {
                        resultList.Results.Add(new Result
                        {
                            DisplayUrl = StringConvert(xElement.Element("url")?.Value),
                            Title = xElement.Element("title")?.Value,
                            Description = element != null ? xElement.Element("headline").Value : xElement.Element("passages").Element("passage").Value,
                            Rank = count++
                        }); 
                    }
                } 
            }

            return resultList;  
        }
    }
}
