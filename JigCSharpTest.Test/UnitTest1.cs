using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using JigCSharp.Parser;

namespace JigCSharpTest.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string inputFileName = @"C:\Repository\DDDSample\DDDSample.DomainModel\Entities\Users.cs";
            using var stream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);
            var classParser = new ClassParser();

            classParser.Generate(stream);
        }
    }
}
