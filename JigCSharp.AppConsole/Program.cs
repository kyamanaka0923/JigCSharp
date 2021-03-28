using System;
using System.IO;
using JigCSharp.AppConsole.Config;
using JigCSharp.AppConsole.Excel;
using JigCSharp.Parser;
using JigCSharp.Parser.SyntaxData.Namespace;
using CommandLine;

namespace JigCSharp.AppConsole
{
    class Program
    {
        class Options
        {
            [Option('c', "config", Required = true, HelpText = "コンフィグレーションファイル")]
            public string ConfigurationFile { get; set; }
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(ParseSolution)
                .WithNotParsed(er => { });
        }

        private static void ParseSolution(Options opt)
        {
            var configFile = opt.ConfigurationFile;

            if (!File.Exists(configFile))
            {
                Console.WriteLine($"指定されたコンフィグファイルが存在ません: {configFile}");
            }

            var configuration = new Configuration(configFile);

            var path = configuration.GetInputPath();
            var outputDir = configuration.GetOutputPath();
            string solutionPath = configuration.GetSolutionPath();

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

            // 全ネームスペース情報を取得
            var allNamespaceDataList = new NamespaceDataList();
            foreach (var file in files)
            {
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                var classParser = new CSharpCodeParser(solutionPath);

                var namespaceDataList = classParser.Generate(stream);

                allNamespaceDataList = allNamespaceDataList.Concat(namespaceDataList);
            }

            var plantumlFilePath = outputDir + @"\plantuml.md";
            var excelFilePath = outputDir + @"\jigAll.xlsx";

            // ネームスペース除外リストから除外
            var targetNamespaceDataList = allNamespaceDataList.Extract(configuration.GetPlantUmlNamespaces());
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