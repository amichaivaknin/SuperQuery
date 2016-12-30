using System.Collections.Generic;
using businessLogic.Extentions;

namespace businessLogic.Models
{
    public class SearchEngineResultsList
    {
        public SearchEngineResultsList()
        {
            Statistics = new Statistics();
        }
        public string SearchEngineName { get; set; }
        public List<Result> Results { get; set; }
        public Statistics Statistics { get; set; }
    }
}