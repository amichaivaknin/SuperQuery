using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
   internal interface IMultiSearch
   {
       IEnumerable<SearchEngineResultsList> GetResultsFromAllSearchEngines(string query);
   }
}
