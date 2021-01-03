using System.Collections.Generic;
using JigCSharp.Parser.SyntaxData.Class;

namespace JigCSharp.Parser.SyntaxData.Namespace
{
    public class NamespaceDto
    {
        public string Name { get; }

        public string DisplayName { get; }
        
        public IEnumerable<ClassDto> ClassDtos { get; }

        public NamespaceDto(string name, string displayName, IEnumerable<ClassDto> classDtos)
        {
            Name = name;
            DisplayName = displayName;
            ClassDtos = classDtos;
        }
    }
}