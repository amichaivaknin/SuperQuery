﻿using businessLogic.SearchEngines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperQueryTests.SearchEnginesTests
{
    /// <summary>
    ///     Summary description for BingSearchEngineTests
    /// </summary>
    [TestClass]
    public class BingSearchEngineTests
    {
        private readonly BingSearchEngine _searchEngine;

        public BingSearchEngineTests()
        {
            _searchEngine = new BingSearchEngine();
        }

        [TestMethod]
        public void BingSearch_NullQuery_ResultsCountEqualTo0()
        {
            var search = _searchEngine.Search("");
            var listCount = search.Results.Count;
            Assert.AreEqual(0, listCount);
        }

        [TestMethod]
        public void BingSearch_JerusalemQuery_ResultsCountBiggerThe0()
        {
            var search = _searchEngine.Search("Jerusalem");
            var listCount = search.Results.Count;
            Assert.IsTrue(listCount > 0);
        }

        [TestMethod]
        public void BingSearch_Ort_Braude_Query_ResultsCountBiggerThe0()
        {
            var search = _searchEngine.Search("Ort Braude");
            var listCount = search.Results.Count;
            Assert.IsTrue(listCount > 0);
        }

        [TestMethod]
        public void BingSearch_How_To_Find_c_Tutorial_Query_ResultsCountBiggerThe0()
        {
            var search = _searchEngine.Search("How To Find c Tutorial");
            var listCount = search.Results.Count;
            Assert.IsTrue(listCount > 0);
        }

        [TestMethod]
        public void BingSearch_QueryWithNoResults_ResultsCountEqualTo0()
        {
            var search = _searchEngine.Search("gfdgdfgdfgdf fdgdfg fgdfgdfgd bdfgdfgdfg bdfgfdgdf fgdfgfd");
            var listCount = search.Results.Count;
            Assert.AreEqual(0, listCount);
        }
    }
}