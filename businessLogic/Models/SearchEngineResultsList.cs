using System.Collections.Generic;

namespace businessLogic.Models
{
    public class SearchEngineResultsList
    {
        public string SearchEngineName { get; set; }
        public List<Result> Results { get; set; }
    }
}