using System;
using System.IO;
using JigCSharp.AppConsole.Config;
using JigCSharp.AppConsole.Excel;
using JigCSharp.Parser;
using JigCSharp.Parser.SyntaxData.Namespace;

namespace JigCSharp.AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 3)
            {
                Console.WriteLine("Usage: JigCSharp.exe [Directory(FullPath)] [OutputDirectory(FullPath)] [ConfigFile]");
                return;
            }

            var path = args[0];
            var outputDir = args[1];
            var configFile = args[2];

            if (!Directory.Exists(path))
            {
                Console.WriteLine($"指定されたディレクトリが存在しません: {path}");
                return;
            }
            
            if (!Directory.Exists(outputDir))
            {
                Console.WriteLine($"指定されたディレクトリが存在しません: {outputDir}");
                return;
            }

            if (!File.Exists(configFile))
            {
                Console.WriteLine($"指定されたコンフィグファイルが存在ません: {configFile}");
            }

            var files = Directory.EnumerateFiles(path, "*.cs", SearchOption.AllDirectories);

            var allNamespaceDataList = new NamespaceDataList();
            foreach (var file in files)
            {
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                var classParser = new CSharpCodeParser();

                var namespaceDataList = classParser.Generate(stream);

                allNamespaceDataList = allNamespaceDataList.Concat(namespaceDataList);
            }

            var plantumlFilePath = outputDir + @"\plantuml.md";
            var excelFilePath = outputDir + @"\jigAll.xlsx";

            using (var plantumlFileStream = new StreamWriter(plantumlFilePath))
            {
                plantumlFileStream.WriteLine(allNamespaceDataList.StringToPlantuml());
            }

            var excludeNamespaces = Configuration.GetExcludeNamespaces(configFile);

            var targetNamespaceDataList = allNamespaceDataList.Exclude(excludeNamespaces);

            ExcelConverter.Convert(targetNamespaceDataList, excelFilePath);
        }
    }
}
