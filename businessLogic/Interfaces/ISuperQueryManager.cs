using System.Collections.Generic;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
    public interface ISuperQueryManager
    {
        IEnumerable<FinalResult> GetQueryResults(string query);

        IEnumerable<FinalResult> GetQueryResults(List<string> engines, string query);
    }
}