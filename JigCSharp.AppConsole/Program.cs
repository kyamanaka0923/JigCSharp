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
            
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: JigCSharp.exe [ConfigFile]");
                return;
            }
            
            var configFile = args[0];
            
            if (!File.Exists(configFile))
            {
                Console.WriteLine($"指定されたコンフィグファイルが存在ません: {configFile}");
            }
            
            var path = Configuration.GetInputPath(configFile);
            var outputDir = Configuration.GetOutputPath(configFile);

            if (!Directory.Exists(path))
            {
                Console.WriteLine($"指定されたディレクトリが存在しません(InputPath): {path}");
                return;
            }
            
            if (!Directory.Exists(outputDir))
            {
                Console.WriteLine($"指定されたディレクトリが存在しません(OutputPath): {outputDir}");
                return;
            }

            var files = Directory.EnumerateFiles(path, "*.cs", SearchOption.AllDirectories);

            // 全ネームスページ情報を取得
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

            // ネームスペース除外リストから除外
            var excludeNamespaces = Configuration.GetExcludeNamespaces(configFile);
            var targetNamespaceDataList = allNamespaceDataList.Exclude(excludeNamespaces);

            // PlantUML形式の結果出力
            using (var plantumlFileStream = new StreamWriter(plantumlFilePath))
            {
                plantumlFileStream.WriteLine(targetNamespaceDataList.StringToPlantuml());
            }

            // Excel形式の結果出力
            ExcelConverter.Convert(targetNamespaceDataList, excelFilePath);
        }
    }
}
