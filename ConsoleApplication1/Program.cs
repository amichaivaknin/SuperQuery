using businessLogic.SearchEngines;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main()
        {

            var r = new RamblerSearchEngine();
            var s = r.AsyncSearch("dfsdgdsgfdbhfghghgfh hgk jkhj hgj hjyjgh jhjhgjghjhgj hj jjhgjhg hjgh   aaaa").Result;
            
            var x = 1;

        }
    }
}
