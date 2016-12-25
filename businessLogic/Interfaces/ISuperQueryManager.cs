using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
    public interface ISuperQueryManager
    {
        IEnumerable<FinalResult> GetQueryResults(string query);

        IEnumerable<FinalResult> GetQueryResults(List<string> engines,string query);
    }
}
