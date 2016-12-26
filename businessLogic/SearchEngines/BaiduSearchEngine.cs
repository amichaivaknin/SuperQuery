using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    internal class BaiduSearchEngine : BaseSearchEngine, ISearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = new SearchEngineResultsList
            {
                SearchEngineName = "Baidu",
                Results = new List<Result>()
            };

            var ch = 'a';

            for (var i = 1; i<11; i++)
            {
                resultList.Results.Add(new Result
                {
                    DisplayUrl = ch.ToString(),
                    Title = ch.ToString(),
                    Description = ch.ToString(),
                    Rank = i
                });
                ch++;
            }

            return resultList;
        }
    }
}
