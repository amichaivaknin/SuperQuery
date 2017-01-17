using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using businessLogic;
using businessLogic.Interfaces;
using businessLogic.Models;
using businessLogic.SearchEngines;
using Markov;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main()
        {
            var ram = new GigaBlastEngine();
            var x = ram.AsyncSearch("amichai");
            var y = 1;
            //var searchEngines = new Dictionary<string, ISearchEngine>
            //{
            //    {"Google", new GoogleSearchEngine()},
            //    {"Bing", new BingSearchEngine()},
            //    {"Yandex", new YandexSearchEngine()},
            //    {"GigaBlast", new GigaBlastEngine()},
            //    {"HotBot", new HotBotSearchEngine()},
            //    {"Rambler", new RamblerSearchEngine()}
            //};

            //var asyncSearchEngines = new Dictionary<string, IAsyncSearchEngine>
            //{
            //    {"Google", new GoogleSearchEngine()},
            //    {"Bing", new BingSearchEngine()},
            //    {"Yandex", new YandexSearchEngine()},
            //    {"GigaBlast", new GigaBlastEngine()},
            //    {"HotBot", new HotBotSearchEngine()},
            //    {"Rambler", new RamblerSearchEngine()}
            //};

            //var parAsync = asyncSearchEngines.Values.AsParallel();

            //var sync = searchEngines.Values.Select(searchengine => searchengine.Search("jerusalem")).ToList();

            //var async = Enumerable.ToList(parAsync.Select(searchEngine => searchEngine.AsyncSearch("jerusalem").Result));

            //var multi = new MultiSearch();


            ////var search = multi.GetResultsFromAllSearchEngines("amichai");
            //var asyncSearch = multi.GetAsyncResultsFromAllSearchEngines("amichai");

            //var x = 1;

            //var dic = new Dictionary<string,int>();
            //var r = new GigaBlastEngine();
            //var s = r.AsyncSearch("amichai").Result;
            //var m = new MarkovChain<Result>(1000);
            //foreach (var res in s.Results)
            //{
            //    var n = new List<Result> {res};
            //    if (res != null) m.Add(n,(int)res.Rank);
            //}
            //dic.Add("fd",1);
            //var f = m.Chain();
            //for (int i = 1; i < 1000; i++)
            //{ 
            //    var b = m.Chain().ToArray();
            //    if (dic.ContainsKey(b[0].DisplayUrl))
            //    {
            //        dic[b[0].DisplayUrl]++;
            //    }
            //    else
            //    {
            //        dic.Add(b[0].DisplayUrl, 1);
            //    }
            //}

            //var q = m.items;
            //var t = m.terminals;
            //var x = 1;

        }
    }
}
