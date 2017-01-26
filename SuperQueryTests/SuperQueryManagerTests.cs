using System.Collections.Generic;
using System.Linq;
using businessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperQueryTests
{
    /// <summary>
    /// Summary description for SuperQueryManagerTests
    /// </summary>
    [TestClass]
    public class SuperQueryManagerTests
    {
        private SuperQueryManager _manager;
        private readonly List<string> _searchEngines;

        public SuperQueryManagerTests()
        {
           _manager = new SuperQueryManager();
           _searchEngines = new List<string>
            {
                "GigaBlast",
                "HotBot",
                "Rambler"
            };
        }

        [TestMethod]
        public void SuperQueryManagerTests_GetQueryResults_NullQuery_ResultsCountEqualTo0()
        {
            var search = _manager.GetQueryResults(_searchEngines, "");
            Assert.IsTrue(!search.Any());
        }

        [TestMethod]
        public void SuperQueryManagerTests_GetQueryResults_JerusalemQuery_ResultsCountBiggerThe0()
        {
            var search = _manager.GetQueryResults(_searchEngines, "Jerusalem");
            Assert.IsTrue(search.Any());
        }

        [TestMethod]
        public void SuperQueryManagerTests_GetQueryResults_JerusalemQuery_ResultsCountEqualTo0()
        {
            var search = _manager.GetQueryResults(_searchEngines, "gfdgdfgdfgdf fdgdfg fgdfgdfgd bdfgdfgdfg bdfgfdgdf fgdfgfd");
            Assert.IsTrue(!search.Any());
        }

        [TestMethod]
        public void SuperQueryManagerTests_GetQueryResultsFromAllSearchEngines_NullQuery_ResultsCountEqualTo0()
        {
            var search = _manager.GetQueryResults("");
            Assert.IsTrue(!search.Any());
        }

        [TestMethod]
        public void SuperQueryManagerTests_GetQueryResultsFromAllSearchEngines_JerusalemQuery_ResultsCountBiggerThe0()
        {
            var search = _manager.GetQueryResults("Jerusalem");
            Assert.IsTrue(search.Any());
        }

        [TestMethod]
        public void SuperQueryManagerTests_GetQueryResultsFromAllSearchEngines_JerusalemQuery_ResultsCountEqualTo0()
        {
            var search = _manager.GetQueryResults("gfdgdfgdfgdf fdgdfg fgdfgdfgd bdfgdfgdfg bdfgfdgdf fgdfgfd");
            Assert.IsTrue(!search.Any());
        }
    }
}
