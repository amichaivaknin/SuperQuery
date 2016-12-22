using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    internal class YahooSearchEngine : BaseSearchEngine, ISearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = new SearchEngineResultsList
            {
                SearchEngineName = "Yahoo",
                Results = new List<Result>()
            };

            var ch = 'a';

            for (var i = 11; i > 0; i--)
            {
                resultList.Results.Add(new Result
                {
                    Link = ch.ToString(),
                    Title = ch.ToString(),
                    PhotoUrl = ch.ToString(),
                    Description = ch.ToString(),
                    Rank = i
                });
                ch++;
            }

            return resultList;
        }
    }
}
