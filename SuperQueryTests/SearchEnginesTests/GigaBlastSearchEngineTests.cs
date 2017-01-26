using System;
using System.Text;
using System.Collections.Generic;
using businessLogic.SearchEngines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperQueryTests.SearchEnginesTests
{
    /// <summary>
    /// Summary description for GigaBlastSearchEngineTests
    /// </summary>
    [TestClass]
    public class GigaBlastSearchEngineTests
    {
        private readonly GigaBlastEngine _searchEngine;
        public GigaBlastSearchEngineTests()
        {
            _searchEngine = new GigaBlastEngine();
        }

        [TestMethod]
        public void GigaBlastSearch_NullQuery_ResultsCountEqualTo0()
        {
            var search = _searchEngine.Search("");
            var listCount = search.Results.Count;
            Assert.AreEqual(0, listCount);
        }

        [TestMethod]
        public void GigaBlastSearch_JerusalemQuery_ResultsCountBiggerThe0()
        {
            var search = _searchEngine.Search("Jerusalem");
            var listCount = search.Results.Count;
            Assert.IsTrue(listCount > 0);
        }

        [TestMethod]
        public void GigaBlastSearch_Ort_Braude_Query_ResultsCountBiggerThe0()
        {
            var search = _searchEngine.Search("Ort Braude");
            var listCount = search.Results.Count;
            Assert.IsTrue(listCount > 0);
        }

        [TestMethod]
        public void GigaBlastSearch_How_To_Find_c_Tutorial_Query_ResultsCountBiggerThe0()
        {
            var search = _searchEngine.Search("How To Find c Tutorial");
            var listCount = search.Results.Count;
            Assert.IsTrue(listCount > 0);
        }

        [TestMethod]
        public void GigaBlastSearch_QueryWithNoResults_ResultsCountEqualTo0()
        {
            var search = _searchEngine.Search("gfdgdfgdfgdf fdgdfg fgdfgdfgd bdfgdfgdfg bdfgfdgdf fgdfgfd");
            var listCount = search.Results.Count;
            Assert.AreEqual(0, listCount);
        }
    }
}