using System.Threading.Tasks;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
    public interface IAsyncSearchEngine
    {
        Task<SearchEngineResultsList> AsyncSearch(string query);
    }
}