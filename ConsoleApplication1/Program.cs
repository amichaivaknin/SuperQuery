using businessLogic.SearchEngines;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main()
        {

            var r = new GoogleSearchEngine();
            var s = r.AsyncSearch("jerusale").Result;
            
            var x = 1;

        }
    }
}
