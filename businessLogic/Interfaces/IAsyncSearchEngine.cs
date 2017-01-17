using System.Threading.Tasks;
using businessLogic.Models;

namespace businessLogic.Interfaces
{
    public interface IAsyncSearchEngine
    {
        SearchEngineResultsList AsyncSearch(string query);
    }
}