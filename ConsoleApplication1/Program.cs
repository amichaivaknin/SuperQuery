using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic;
using businessLogic.Interfaces;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ISuperQueryManager x = new SuperQueryManager();
            var y = x.GetQueryResults("Amichai");
            foreach (var result in y)
            {
                Console.WriteLine(result.SearchEngines +" " +result.DisplayUrl+ " " +result.Rank);
            }

            Console.ReadLine();

        }
    }
}
