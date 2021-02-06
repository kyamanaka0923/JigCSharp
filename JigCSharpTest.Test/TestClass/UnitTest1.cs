using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using JigCSharp.Parser;

namespace JigCSharpTest.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string solutionPath = @"C:\Repository\DDDSample\DDDSample.sln";
        [TestMethod]
        public void TestMethod1()
        {
            const string inputFileName = @"C:\Repository\DDDSample\DDDSample.DomainModel\Entities\Users.cs";
            using var stream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);
            var classParser = new CSharpCodeParser(solutionPath);

            var namespaceDataList = classParser.Generate(stream);

            var result = namespaceDataList.StringToPlantuml();
            Console.WriteLine(result);
        }

        [TestMethod]
        ///
        public void DisplayListTest()
        {
            const string inputFileName = @"C:\Repository\DDDSample\DDDSample.DomainModel\Entities\Users.cs";
            using var stream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);
            var classParser = new CSharpCodeParser(solutionPath);

            var namespaceDataList = classParser.Generate(stream);

            var result = namespaceDataList.DisplayList();
            Console.WriteLine(result);
        }
    }
}
