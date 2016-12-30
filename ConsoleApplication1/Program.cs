using businessLogic.SearchEngines;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main()
        {

            var r = new RamblerSearchEngine();
            var s = r.AsyncSearch("amichai").Result;

            var y = (double)s.Statistics.Duration;
            var x = 1;

        }
    }
}
