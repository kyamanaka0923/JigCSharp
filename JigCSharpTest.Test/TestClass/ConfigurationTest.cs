using System.Linq;
using JigCSharp.AppConsole.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace JigCSharpTest.Test
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void GetExcludeNamespacesTest()
        {
            var excludedNamespaces = Configuration.GetExcludeNamespaces("./TestData/configTest.json");

            excludedNamespaces.Single(x => x == "aaa");
            excludedNamespaces.Single(x => x == "bbb");
        }
        
    }
}