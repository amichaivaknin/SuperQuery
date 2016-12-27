using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
    internal interface IRankAggregation
    {
        //List<FinalResult> RankAndMerge(IEnumerable<SearchEngineResultsList> allSearchResults);
        List<FinalResult> BordaRank(IEnumerable<SearchEngineResultsList> allSearchResults);
    }
}
