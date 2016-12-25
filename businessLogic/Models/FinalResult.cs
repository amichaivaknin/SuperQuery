using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.Models
{
    public class FinalResult : Result
    {
        public FinalResult()
        {
            SearchEngines = new Dictionary<string, int>();
        }
        public Dictionary<string,int> SearchEngines { get; internal set; }
    }
}
