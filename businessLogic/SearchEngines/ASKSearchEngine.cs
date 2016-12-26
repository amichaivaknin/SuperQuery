using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace businessLogic.SearchEngines
{
    internal class AskSearchEngine : BaseSearchEngine, ISearchEngine
    {
        public AskSearchEngine() 
        {
        }

        public SearchEngineResultsList Search(string query)
        {
            var resultList = new SearchEngineResultsList
            {
                SearchEngineName = "ASK",
                Results = new List<Result>()
            };

            var ch = 'b';

            for (var i = 11; i > 0; i--)
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
