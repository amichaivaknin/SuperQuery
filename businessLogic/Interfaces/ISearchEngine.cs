using businessLogic.Models;

namespace businessLogic.Interfaces
{
    internal interface ISearchEngine
    {
        SearchEngineResultsList Search(string query);
    }
}