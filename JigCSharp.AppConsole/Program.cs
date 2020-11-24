using System;
using System.IO;
using JigCSharp.Parser;

namespace JigCSharp.AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 1)
            {
                Console.WriteLine("Usage: JigCSharp.exe [Directory(FullPath)]");
                return;
            }

            var path = args[0];

            if (!Directory.Exists(path))
            {
                Console.WriteLine($"指定されたディレクトリが存在しません: {path}");
                return;
            }

            var files = Directory.EnumerateFiles(path, "*.cs", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                var classParser = new ClassParser();

                classParser.Generate(stream);
            }
        }
    }
}
