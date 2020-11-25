using System;
using System.Collections.Generic;
using System.Text;
using JigCSharp.Parser.SyntaxData.Class;
using JigCSharp.Parser.SyntaxData.Common;

namespace JigCSharp.Parser.SyntaxData.Namespace
{
    public class NamespaceData
    {
        public DeclarationName Name { get; }

        private readonly List<ClassOrInterfaceData> _classDataList;

        public NamespaceData(DeclarationName name)
        {
            Name = name;
            _classDataList = new List<ClassOrInterfaceData>();
        }

        public void AddClassData(ClassOrInterfaceData classOrInterfaceData)
        {
            _classDataList.Add(classOrInterfaceData);
        }

        public string DisplayPackage()
        {
            var returnStringBuilder = new StringBuilder();

            returnStringBuilder.AppendLine($"package \"{Name.Name}{Name._displayName}\"{{");
            foreach (var classData in _classDataList)
            {
                returnStringBuilder.AppendLine(classData.Display());
            }
            returnStringBuilder.AppendLine("}");
            return returnStringBuilder.ToString();
        }
        public string DisplayAssociation()
        {
            var returnStringBuilder = new StringBuilder();

            foreach (var classData in _classDataList)
            {
                returnStringBuilder.AppendLine(classData.DisplayAccess());
            }

            return returnStringBuilder.ToString();
        }
    }
}
