using System.Collections.Generic;
using System.Linq;
using JigCSharp.Parser.SyntaxData.Common;
using JigCSharp.Parser.SyntaxData.Namespace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;

namespace JigCSharpTest.Test
{
    [TestClass]
    public class NamespaceDataListTest
    {
        [TestMethod]
        public void ExcludeTest()
        {
            var namespaceDataList = new NamespaceDataList();

            namespaceDataList.Add(new NamespaceData(new DeclarationName("aaa.bbb.ccc", "other")));
            namespaceDataList.Add(new NamespaceData(new DeclarationName("bbb.bbb.ccc", "other")));
            namespaceDataList.Add(new NamespaceData(new DeclarationName("ccc.bbb.ccc", "other")));

            var excludeDate = new List<string>()
            {
                "aaa.bbb",
                "ccc"
            };

            var targetNamespace = namespaceDataList.Exclude(excludeDate);

            Assert.AreEqual(1, targetNamespace.ToEnumerable().Count());
            Assert.AreEqual("bbb.bbb.ccc", targetNamespace.ToEnumerable().Single(x => x.Name.Name == "bbb.bbb.ccc").Name.Name);

        }
        
    }
}