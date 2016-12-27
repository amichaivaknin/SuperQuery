using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic;
using businessLogic.Interfaces;
using businessLogic.SearchEngines;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var r =new RamblerSearchEngine();
            var s = r.Search1("amichai").Result;

        }
    }
}
