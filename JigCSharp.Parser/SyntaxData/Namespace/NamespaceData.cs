using System;
using System.Collections.Generic;
using JigCSharp.Parser.SyntaxData.Class;
using JigCSharp.Parser.SyntaxData.Common;

namespace JigCSharp.Parser.SyntaxData.Namespace
{
    public class NamespaceData
    {
        public DeclarationName Name { get; }

        private readonly List<ClassData> _classDataList;

        public NamespaceData(DeclarationName name)
        {
            Name = name;
            _classDataList = new List<ClassData>();
        }

        public void AddClassData(ClassData classData)
        {
            _classDataList.Add(classData);
        }

        public void Display()
        {
            Console.WriteLine($"package \"{Name.Name}{Name._displayName}\"{{");
            foreach (var classData in _classDataList)
            {
                classData.Display();
            }
            Console.WriteLine("}");
            foreach (var classData in _classDataList)
            {
                classData.DisplayAccess();
            }

        }
    }
}
