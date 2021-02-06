using JigCSharp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JigCSharpTest.Test
{
    [TestClass]
    public class ReferenceFinderTest
    {
        [TestMethod]
        public void FindTest()
        {
            var finder = new ReferenceFinder();
            
            finder.Find("aaa", @"C:\Repository\JigCSharp\JigCSharp.sln");
        }
        
    }
}