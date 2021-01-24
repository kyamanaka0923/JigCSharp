using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace JigCSharp.AppConsole.Config
{
    public class Configuration
    {
        public static string GetInputPath(string configPath)
        {
            var configEntity = GetConfig(configPath);

            return configEntity.InputDir;
        }

        public static string GetOutputPath(string configPath)
        {
            var configEntity = GetConfig(configPath);

            return configEntity.OutputDir;
        }
        
        public static IEnumerable<string> GetExcludeNamespaces(string configPath)
        {
            var configEntity = GetConfig(configPath);

            return configEntity.ExcludeNamespace;
        }

        private static ConfigEntity GetConfig(string configPath)
        {
            var jsonString = ReadAllLine(configPath, "utf-8");

            return JsonSerializer.Deserialize<ConfigEntity>(jsonString);
        }

        private static string ReadAllLine(string filePath, string encodingName)
        {
            StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(encodingName));
            string allLine = sr.ReadToEnd();
            sr.Close();

            return allLine;
        }

        public class ConfigEntity
        {
            public string Name { get; set; }
            
            /// <summary>
            /// 生成対象のファイルが格納されているディレクトリ
            /// </summary>
            public string InputDir { get; set; }
            
            /// <summary>
            /// 結果出力先ディレクトリ
            /// </summary>
            public string OutputDir { get; set; }
            
            /// <summary>
            /// 除外対象ネームスペース一覧
            /// </summary>
            public IEnumerable<string> ExcludeNamespace { get; set; }
        }
    }
}