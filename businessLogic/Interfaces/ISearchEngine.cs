using businessLogic.Models;

namespace businessLogic.Interfaces
{
    public interface ISearchEngine
    {
        SearchEngineResultsList Search(string query);
    }
}