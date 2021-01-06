using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace JigCSharp.AppConsole.Config
{
    public class Configuration
    {
        public static IEnumerable<string> GetExcludeNamespaces(string configPath)
        {
            var jsonString = ReadAllLine(configPath, "utf-8");

            var configEntity =  JsonSerializer.Deserialize<ConfigEntity>(jsonString);

            return configEntity.ExcludeNamespace;
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
            public IEnumerable<string> ExcludeNamespace { get; set; }
        }
    }
}