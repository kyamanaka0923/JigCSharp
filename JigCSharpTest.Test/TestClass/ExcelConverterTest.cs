using System.IO;
using JigCSharp.AppConsole.Excel;
using JigCSharp.Parser;
using JigCSharp.Parser.SyntaxData.Namespace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JigCSharpTest.Test
{
    [TestClass]
    public class ExcelConverterTest
    {
        //const string inputDir = @"C:\Repository\DDDSample\DDDSample.DomainModel";
        private const string inputDir = @"C:\Repository\JigCSharp";
        private const string solutionPath = @"C:\Repository\JigCSharp\JigCSharp.sln";
        [TestMethod]
        public void ConvertTest()
        {
            var files = Directory.EnumerateFiles(inputDir, "*.cs", SearchOption.AllDirectories);

            var allNamespaceDataList = new NamespaceDataList();
            foreach (var file in files)
            {
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                var classParser = new CSharpCodeParser(solutionPath);

                var namespaceDataList = classParser.Generate(stream);

                allNamespaceDataList = allNamespaceDataList.Concat(namespaceDataList);
            }
            ExcelConverter.Convert(allNamespaceDataList, @"C:\Repository\JigCSharp\work\output.xlsx");

            var namespaceData = allNamespaceDataList.DisplayList();
        }
        
    }
}