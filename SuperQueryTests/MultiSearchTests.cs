using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using businessLogic;
using businessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperQueryTests
{
    /// <summary>
    /// Summary description for MultiSearchTests
    /// </summary>
    [TestClass]
    public class MultiSearchTests
    {
        private readonly MultiSearch _multiSearch;
        private readonly List<string> _searchEngines;

        public MultiSearchTests()
        {
            _multiSearch = new MultiSearch();
            _searchEngines = new List<string>
            {
                "GigaBlast",
                "HotBot",
                "Rambler"
            };
        }

        [TestMethod]
        public void multiSearch_GetResultsFromSelectedSearchEngines_NullQuery_ResultsCountEqualTo0()
        {
            var search = _multiSearch.GetResultsFromSelectedSearchEngines(_searchEngines,"");
            var searchEngineResultsLists = search as IList<SearchEngineResultsList> ?? search.ToList();
            var listCount = searchEngineResultsLists.Count;

            Assert.AreEqual(_searchEngines.Count, listCount);
            foreach (var engine in searchEngineResultsLists)
            {
                Assert.AreEqual(0,engine.Results.Count, engine.SearchEngineName);
            }
        }

        [TestMethod]
        public void multiSearch_GetResultsFromSelectedSearchEngines_Ort_Braude_Query_ResultsCountBiggerThe0()
        {
            var search = _multiSearch.GetResultsFromSelectedSearchEngines(_searchEngines,"Ort Braude");
            var searchEngineResultsLists = search as IList<SearchEngineResultsList> ?? search.ToList();
            var listCount = searchEngineResultsLists.Count;

            Assert.AreEqual(_searchEngines.Count, listCount);
            foreach (var engine in searchEngineResultsLists)
            {
                Assert.IsTrue(listCount > 0, engine.SearchEngineName);
            }
        }

        [TestMethod]
        public void multiSearch_GetResultsFromSelectedSearchEngines_How_To_Find_c_Tutorial_Query_ResultsCountBiggerThe0()
        {
            var search = _multiSearch.GetResultsFromSelectedSearchEngines(_searchEngines,"How To Find c Tutorial");
            var searchEngineResultsLists = search as IList<SearchEngineResultsList> ?? search.ToList();
            var listCount = searchEngineResultsLists.Count;

            Assert.AreEqual(_searchEngines.Count, listCount);
            foreach (var engine in searchEngineResultsLists)
            {
                Assert.IsTrue(listCount > 0, engine.SearchEngineName);
            }
        }

        [TestMethod]
        public void multiSearch_GetResultsFromSelectedSearchEngines_QueryWithNoResults_ResultsCountEqualTo0()
        {
            var search = _multiSearch.GetResultsFromSelectedSearchEngines(_searchEngines,"gfdgdfgdfgdf fdgdfg fgdfgdfgd bdfgdfgdfg bdfgfdgdf fgdfgfd");
            var searchEngineResultsLists = search as IList<SearchEngineResultsList> ?? search.ToList();
            var listCount = searchEngineResultsLists.Count;

            Assert.AreEqual(_searchEngines.Count, listCount);
            foreach (var engine in searchEngineResultsLists)
            {
                Assert.AreEqual(0, engine.Results.Count, engine.SearchEngineName);
            }
        }

        [TestMethod]
        public void multiSearch_GetResultsFromAllSearchEngines_NullQuery_ResultsCountEqualTo0()
        {
            var search = _multiSearch.GetResultsFromAllSearchEngines("");
            var searchEngineResultsLists = search as IList<SearchEngineResultsList> ?? search.ToList();
            var listCount = searchEngineResultsLists.Count;

            Assert.AreEqual(6, listCount);
            foreach (var engine in searchEngineResultsLists)
            {
                Assert.AreEqual(0, engine.Results.Count, engine.SearchEngineName);
            }
        }

        [TestMethod]
        public void multiSearch_GetResultsFromAllSearchEngines_Ort_Braude_Query_ResultsCountBiggerThe0()
        {
            var search = _multiSearch.GetResultsFromAllSearchEngines("Ort Braude");
            var searchEngineResultsLists = search as IList<SearchEngineResultsList> ?? search.ToList();
            var listCount = searchEngineResultsLists.Count;

            Assert.AreEqual(6, listCount);
            foreach (var engine in searchEngineResultsLists)
            {
                Assert.IsTrue(listCount > 0, engine.SearchEngineName);
            }
        }

        [TestMethod]
        public void multiSearch_GetResultsFromAllSearchEngines_How_To_Find_c_Tutorial_Query_ResultsCountBiggerThe0()
        {
            var search = _multiSearch.GetResultsFromAllSearchEngines( "How To Find c Tutorial");
            var searchEngineResultsLists = search as IList<SearchEngineResultsList> ?? search.ToList();
            var listCount = searchEngineResultsLists.Count;

            Assert.AreEqual(6, listCount);
            foreach (var engine in searchEngineResultsLists)
            {
                Assert.IsTrue(listCount > 0, engine.SearchEngineName);
            }
        }
    }
}
