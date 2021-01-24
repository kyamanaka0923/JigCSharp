using System.Linq;
using JigCSharp.AppConsole.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace JigCSharpTest.Test
{
    [TestClass]
    public class ConfigurationTest
    {
        private string configPath = "./TestData/configTest.json";
        
            
        [TestMethod]
        public void GetExcludeNamespacesTest()
        {
            var excludedNamespaces = Configuration.GetExcludeNamespaces(configPath);

            excludedNamespaces.Single(x => x == "aaa");
            excludedNamespaces.Single(x => x == "bbb");
        }

        [TestMethod]
        public void GetInputPuthTest()
        {
            var inputDir = Configuration.GetInputPath(configPath);

            Assert.AreEqual("input", inputDir);
        }

        [TestMethod]
        public void GetOutputPathTest()
        {
            var outputDir = Configuration.GetOutputPath(configPath);
            
            Assert.AreEqual("output", outputDir);
        }
        
    }
}