using System;
using System.IO;
using JigCSharp.AppConsole.Excel;
using JigCSharp.Parser;
using JigCSharp.Parser.SyntaxData.Namespace;

namespace JigCSharp.AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 2)
            {
                Console.WriteLine("Usage: JigCSharp.exe [Directory(FullPath)] [option(plantuml/list)]");
                return;
            }

            var path = args[0];
            var option = args[1];

            if (!Directory.Exists(path))
            {
                Console.WriteLine($"指定されたディレクトリが存在しません: {path}");
                return;
            }

            if (!((option == "plantuml") || (option == "list")))
            {
                Console.WriteLine($"オプションが指定されていません: plantuml/list");
                return;
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

            switch (option)
            {
                case "plantuml":
                    Console.WriteLine("```plantuml");
                    Console.WriteLine(allNamespaceDataList.StringToPlantuml());
                    Console.WriteLine("```");
                    break;

                case "list":
                    Console.WriteLine(allNamespaceDataList.DisplayList());
                    break;
                
                case "excel":
                    Console.WriteLine("Excel出力");
                    ExcelConverter.Convert(allNamespaceDataList, @"C:\Repository\JigCSharp\work\output.xlsx",
                        @"C:\Repository\JigCSharp\work\template.xlsx");
                    break;
            }
        }
    }
}
