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
            var config = new Configuration(configPath);
            var excludedNamespaces = config.GetExcludeNamespaces();

            excludedNamespaces.Single(x => x == "aaa");
            excludedNamespaces.Single(x => x == "bbb");
        }

        [TestMethod]
        public void GetInputPuthTest()
        {
            var config = new Configuration(configPath);
            var inputDir = config.GetInputPath();

            Assert.AreEqual("input", inputDir);
        }

        [TestMethod]
        public void GetOutputPathTest()
        {
            var config = new Configuration(configPath);
            var outputDir = config.GetOutputPath();
            
            Assert.AreEqual("output", outputDir);
        }
        
    }
}