using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace JigCSharp.AppConsole.Config
{
    public class Configuration
    {
        private readonly ConfigEntity _configEntity;
        public Configuration(string configPath)
        {
            _configEntity = GetConfig(configPath);

        }
        public string GetInputPath()
        {
            return _configEntity.InputDir;
        }

        public string GetOutputPath()
        {
            return _configEntity.OutputDir;
        }
        
        public IEnumerable<string> GetExcludeNamespaces()
        {
            return _configEntity.ExcludeNamespace;
        }

        public IEnumerable<string> GetPlantUmlNamespaces()
        {
            return _configEntity.PlantumlNamespace;
        }

        private ConfigEntity GetConfig(string configPath)
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
        
        public string GetSolutionPath()
        {
            return _configEntity.SolutionPath;
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
            /// Solutionファイルパス
            /// </summary>
            public string SolutionPath { get; set; }
            
            /// <summary>
            /// 除外対象ネームスペース一覧
            /// </summary>
            public IEnumerable<string> ExcludeNamespace { get; set; }
            
            /// <summary>
            /// PlantUML対象ネームスペース一覧
            /// </summary>
            public IEnumerable<string> PlantumlNamespace { get; set; }
            
        }

    }
}