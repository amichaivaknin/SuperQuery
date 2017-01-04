using System;
using System.Text;
using System.Collections.Generic;
using businessLogic.SearchEngines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperQueryTests.SearchEnginesTests
{
    /// <summary>
    /// Summary description for YandexSearchEngineTests
    /// </summary>
    [TestClass]
    public class YandexSearchEngineTests
    {
        private readonly YandexSearchEngine _searchEngine;
        public YandexSearchEngineTests()
        {
            _searchEngine = new YandexSearchEngine();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void YandexSearch_NullQuery_ResultsCountEqualTo0()
        {
            var search = _searchEngine.Search("");
            var listCount = search.Results.Count;
            Assert.AreEqual(0, listCount);
        }

        [TestMethod]
        public void YandexSearch_JerusalemQuery_ResultsCountBiggerThe0()
        {
            var search = _searchEngine.Search("Jerusalem");
            var listCount = search.Results.Count;
            Assert.IsTrue(listCount > 0);
        }

        [TestMethod]
        public void YandexSearch_QueryWithNoResults_ResultsCountEqualTo0()
        {
            var search = _searchEngine.Search("gfdgdfgdfgdf fdgdfg fgdfgdfgd bdfgdfgdfg bdfgfdgdf fgdfgfd");
            var listCount = search.Results.Count;
            Assert.AreEqual(0, listCount);
        }

        //[TestMethod]
        //public void YandexAsyncSearch_NullQuery_ResultsCountEqualTo0()
        //{
        //    var search = _searchEngine.AsyncSearch("").Result;
        //    var listCount = search.Results.Count;
        //    Assert.AreEqual(0, listCount);
        //}

        //[TestMethod]
        //public void YandexAsyncSearch_JerusalemQuery_ResultsCountBiggerThe0()
        //{
        //    var search = _searchEngine.AsyncSearch("Jerusalem").Result;
        //    var listCount = search.Results.Count;
        //    Assert.IsTrue(listCount > 0);
        //}

        //[TestMethod]
        //public void YandexAsynSearch_QueryWithNoResults_ResultsCountEqualTo0()
        //{
        //    var search = _searchEngine.AsyncSearch("gfdgdfgdfgdf fdgdfg fgdfgdfgd bdfgdfgdfg bdfgfdgdf fgdfgfd").Result;
        //    var listCount = search.Results.Count;
        //    Assert.AreEqual(0, listCount);
        //}
    }
}
