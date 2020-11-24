using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace JigCSharp.Parser
{
    class XmlDocument
    {
        public string Summary { get; }

        XmlDocument(string summary)
        {
            Summary = summary;
        }

        public static XmlDocument Parse(string xmlDocument)
        {
            if(string.IsNullOrEmpty(xmlDocument)) return new XmlDocument("");
            var document = XDocument.Parse(xmlDocument);
            var summary = document.Descendants("summary").FirstOrDefault();

            return summary == null ? new XmlDocument("") : new XmlDocument(summary.Value.Trim());
        }
    }
}
