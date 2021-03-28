using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace JigCSharp.Parser
{
    public class XmlDocument
    {
        public string Summary { get; }
        public List<ParamComment> ParamCommentList { get; }
        
        XmlDocument(string summary, IEnumerable<ParamComment> paramCommentList)
        {
            Summary = summary;
            ParamCommentList = new List<ParamComment>(paramCommentList);
        }

        public static XmlDocument Parse(string xmlDocument)
        {
            // Summaryを取得する
            if (string.IsNullOrEmpty(xmlDocument))
            {
                return new XmlDocument("", new List<ParamComment>());
            }
            
            var document = XDocument.Parse(xmlDocument);
            var summary = document.Descendants("summary").FirstOrDefault();

            var summaryName = (summary == null) ? "" : summary.Value.Trim();
            
            // Param要素を取得する
            var paramList = document.Descendants("param");
            var paramCommentList = new List<ParamComment>();
            foreach (var param in paramList)
            {
                var name = param.Attribute("name");
                if (name != null)
                {
                    var paramComment = new ParamComment(name.Value, param.Value);
                    paramCommentList.Add(paramComment);
                }
            }
            
            return new XmlDocument(summaryName, paramCommentList);
        }

        public class ParamComment
        {
            public string Name { get; }
            public string AliasName { get; }

            internal ParamComment(string name, string aliasName)
            {
                Name = name;
                AliasName = aliasName;
            }
        }
    }
}
