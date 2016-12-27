using businessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Models;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace businessLogic.SearchEngines
{
    class GigaBlastEngine : BaseSearchEngine, ISearchEngine
    {
        public SearchEngineResultsList Search(string query)
        {
            var resultList = CreateSearchEngineResultsList("GigaBlast");
          
            var count = 1;
            WebClient webClient = new WebClient();
            string result = webClient.DownloadString(string.Format("http://www.gigablast.com/search?q={0}&format=json&n=10&rxivq=1015471771&rand=1482683517796",query));
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> collection = serializer.Deserialize<Dictionary<string, object>>(result);

            foreach (Dictionary<string, object> res in (IEnumerable)collection["results"])
            {
                if (!resultList.Results.Any(r => r.DisplayUrl.Equals(StringConvert(res["url"].ToString()))))
                {
                    resultList.Results.Add(new Result
                    {
                        DisplayUrl = StringConvert(res["url"].ToString()),
                        Title = res["title"].ToString(),
                        Description = res["sum"].ToString(),
                        Rank = count++
                    });
                }        
            }
            return resultList;
        }
    }
}
