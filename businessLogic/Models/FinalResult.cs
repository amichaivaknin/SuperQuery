using System.Collections.Generic;

namespace businessLogic.Models
{
    /// <summary>
    /// FinalResult class it's the result that return to the user
    /// inheritance from Result class
    /// </summary>
    public class FinalResult : Result
    {
        public FinalResult()
        {
            SearchEngines = new Dictionary<string, int>();
        }
        public Dictionary<string, int> SearchEngines { get; internal set; } // show where its shown and what is the rank
    }
}