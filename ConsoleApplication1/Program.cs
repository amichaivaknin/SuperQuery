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
            var y = x.GetQueryResults("11");

            foreach (var result in y)
            {
                Console.WriteLine(result.SearchEngines +" " +result.Link+ " " +result.Rank);
            }

            Console.ReadLine();

        }
    }
}
