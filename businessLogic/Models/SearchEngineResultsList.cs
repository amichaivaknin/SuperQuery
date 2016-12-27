using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.Models
{
    public class SearchEngineResultsList
    {
        public string SearchEngineName { get; set; }
        public List<Result> Results { get; set; }
    }
}
