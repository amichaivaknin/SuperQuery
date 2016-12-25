using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using businessLogic.Models;
using Google.Apis.Services;

namespace businessLogic.SearchEngines
{
    internal class GoogleSearchEngine : BaseSearchEngine, ISearchEngine
    {
        private const string ApiKey = "AIzaSyDAGFKL3kZevjzrFizgnVGnKmZNKUM1hjw";
        private const string SearchEngineId = "007172875963593911035:kpk5tcwf8pa";

        public GoogleSearchEngine() 
        {
           
        }

        public SearchEngineResultsList Search(string query)
        {
            var resultList = new SearchEngineResultsList
            {
                SearchEngineName = "Google",
                Results = new List<Result>()
            };
            var svc = new Google.Apis.Customsearch.v1.CustomsearchService(new BaseClientService.Initializer { ApiKey = ApiKey });
            var listRequest = svc.Cse.List(query);

            listRequest.Cx = SearchEngineId;
            var search = listRequest.Execute();
            
            foreach (var item in search.Items.Select(result => new Result
            {
                Link = result.Link,
                Title = result.Title,
                PhotoUrl = "hghg",
                Description = result.HtmlSnippet,
                Rank = 1,
            }))
            {
                resultList.Results.Add(item);
            }

            return resultList;

            //
            //{
            //    SearchEngineName = "Google",
            //    Results = new List<Result>()
            //};

            //var ch = 'a';

            //for (var i = 11; i > 0; i--)
            //{
            //    resultList.Results.Add(new Result
            //    {
            //        Link = ch.ToString(),
            //        Title = ch.ToString(),
            //        PhotoUrl = ch.ToString(),
            //        Description = ch.ToString(),
            //        Rank = i
            //    });
            //    ch++;
            //}

            //return resultList;
        }
    }
}
